using System;
using System.Collections.Generic;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;
using Xamarin.Forms;
using SEEA;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Plugin.TextToSpeech;
using System.Collections;
using System.IO;
using System.Linq;
using FFImageLoading;
using Xamarin.Forms.Internals;

namespace SEEA.TalkToSeea
{
    public partial class TalkToSeeaScreen : ContentPage
    {
        private ISpeechToText speechRecongnitionInstance;
        private Chatbot chatbot;

        private readonly Color Seea_talk_text_color = Color.FromHex("#AF4444");
        private readonly Color User_talk_text_color = Color.White;
        //        private readonly Color Seea_talk_text_color = Color.FromHex(Application.Current.Resources["BrandColor"].ToString());

        private readonly Color Seea_talk_bg_color = Color.White;
        private readonly Color User_talk_bg_color = Color.FromHex("#AF4444");
        //        private readonly Color User_talk_bg_color = Color.FromHex(Application.Current.Resources["BrandColor"].ToString());

        private readonly Color Seea_talk_border_color = Color.FromHex("#AF4444");
        private readonly Color User_talk_border_color = Color.White;
        //        private readonly Color Seea_talk_border_color = Color.FromHex(Application.Current.Resources["BrandColor"].ToString());

        List<String> inputs = new List<String>();
        String platform = Device.RuntimePlatform;
      
        public TalkToSeeaScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            header.Text = AppResources.TalkToSeeaHeader;
            user_talk_entry.Placeholder = AppResources.user_talk_entry;

            add_new_seea_msg("Hello "+ProfileHomeScreen.USER_NAME+", you can add your favorite Tags here by writing/saying (I love <Tag name>) or (I like <Tag name>)");

            chatbot = new Chatbot();
            InitilizeVoiceServices();

            //TODO:solve this, it should scroll to the bottom of the scrollview
            user_talk_entry.Focused += (s, e) =>
            {
                SeeaTalkFrame lastChild = chat_messages_stack.Children.ElementAt(chat_messages_stack.Children.Count-1) as SeeaTalkFrame;
                chat_message_scrollview.ScrollToAsync(lastChild, ScrollToPosition.MakeVisible, true);
            };
        }

       
        public async void returnToHomePage(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }


        public async void returnToPreviousPage(object sender, EventArgs args)
        {
            await Navigation.PopToRootAsync();
        }





        // ****** SEEA simple AI ******** /// 
        /**
       * This method initilizes the voice services API avaliable on the device whether iOS or Android. refer to the native iOS and Android projects to see the classes
       * that were implemented their as well.
       */
        public void InitilizeVoiceServices() {

            try
            {
                speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                Log.Warning(ex.Message, "");
                //add_new_seea_msg(ex.Message);
            }

            MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);
            });

            MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            {
                mic_btn.IsEnabled = true;

                //this triggers twice at the end, so the last input is duplicated, once for the last detected word where the last word in the sentence is being detected,
                //and the other one when the plugin returned the full detected sentence
                //, in this case we had to wait till the last two inputs are the same
                //then use the last one and empty the list so it will not used the previous input when a new input is entered.
                if ((inputs.Count != 0))
                {
                    if (inputs.ElementAt(inputs.Count - 1) == inputs.ElementAt(inputs.Count - 2))
                    {
                        if (platform == "iOS")
                        {
                            mic_btn.Source = "googleMic.png";
                            add_new_user_msg(inputs.ElementAt(inputs.Count - 1));
                            TriggerSeeaRespond(inputs.ElementAt(inputs.Count - 1));
                            inputs = new List<String>();
                        }
                    }
                }
            });

            //Send the final result to the response generation initiation method.
            MessagingCenter.Subscribe<IIMessageSender, string>(this, "STT", (sender, args) =>
            {
                SpeechToTextFinalResultRecieved(args);

            });
        }

        /**
        * This method is activated as soon as it recieves the full length user input to initiate the response creation process.
        * @param args which is the user input in a textual format, no matter the way the user really entered it either via text or voice.
        */
        private void SpeechToTextFinalResultRecieved(string args)
        {
            if (platform == "iOS")
            {
                inputs.Add(args);
            }

            else
            {
                //call the method that adds the user input into the stacklayou.
                add_new_user_msg(args);

                //set the correct image that shows that the mic is not listening anymore.
                speechRecongnitionInstance.StopSpeechToText();
                mic_btn.Source = "googleMic.png";

                //call the method that triggers SEEA response creation.
                TriggerSeeaRespond(args);
            }
        }

        /**
        * This method calls the method that careates SEEA respond, then adds it to the stacklayout to be shown on the screen.
        *
        * @param userInput in a textual format.
        */
        public void TriggerSeeaRespond(String userInput) {

            //call the method to create the response.
            String seeaRespond = chatbot.CreateSEEAResponse(userInput);

            //add the response to the screen by calling SEEA side adding method.
            add_new_seea_msg(seeaRespond);

            //speak out loud the response if the user input was via voice.
            if (input_text.IsVisible == false) {
                CrossTextToSpeech.Current.Speak(seeaRespond);
            }
        }

       

        /// ********************************* //////////


        //// UI Handlers ///////
        /**
        * This method initiates the listening process.
        *
        * @param sender that initiated the process.
        * @param e which contains the arguments of the initiator.
        */
        private void Mic_btn_Clicked(object sender, EventArgs e)
        {
            //listen to the user via voice.
            try
            {
                speechRecongnitionInstance.StartSpeechToText();

                //change the mic image to indicate that the listening process is active.
                mic_btn.Source = "recordingSign.png";
            }
            catch (Exception ex)
            {
                //show the error message as a SEEA response if any.
                add_new_seea_msg(ex.Message);
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                mic_btn.IsEnabled = false;
            }
        }

        /**
        * This method handels the action of clicking on the key board button. it changes the input type from voice to text.
        *
        * @param sender that initiated the process.
        * @param e which contains the arguments of the initiator.
        */
        private void Kb_btn_Clicked(object sender, EventArgs e)
        {
            //activate the text input.
            input_text.IsVisible = true;

            //deactivate the voice input.
            input_default.IsVisible = false;
        }

        /**
        * This method handels the action of clicking on the send button. Input is text only.
        *
        * @param sender that initiated the process.
        * @param e which contains the arguments of the initiator.
        */
        private void Send_Text_Button_Clicked(object sender, EventArgs e)
        {
            //trim the textual input.
            String tmp_text = user_talk_entry.Text.Trim();

            //if the input was not empty
            if (tmp_text.Length > 0)
            {
                //add the input to the stacklayout to be shown on the screen.
                add_new_user_msg(tmp_text);

                //empty the input field.
                user_talk_entry.Text = "";
            }
            //trigger SEEA reply here.
            TriggerSeeaRespond(tmp_text);
        }

        /**
        * This method prepares the screen with the right UI elements, when the user clicks on the mic button from the keyboard view (Text Mode -> Voice Mode).
        *
        * @param sender that initiated the process.
        * @param e which contains the arguments of the initiator.
        */
        private void Hide_text_input_Clicked(object sender, EventArgs e)
        {
            //clear the content of the user input field as it is no longer relevant.
            user_talk_entry.Text = "";

            //hide the text input field and show the voice elements instead.
            input_text.IsVisible = false;
            input_default.IsVisible = true;
        }

        /**
        * This method adds new user message to the stacklayout to be shown on the screen.
        *
        * @param msg which the message that were entered by the user either directly textual, or via voice then converted into text.
        */
        private void add_new_user_msg(String msg) {

            //construct a user chat frame with the correct colors.
            UserTalkFrame new_msg = new UserTalkFrame(User_talk_bg_color, User_talk_text_color, User_talk_border_color);
            //set the message entered inside the frame constructed.
            new_msg.Text = msg;

            //Add the frame to the stacklayout.
            chat_messages_stack.Children.Add(new_msg);
            //scroll to the end of the screen to include the added message (frame) in the scope.
            chat_message_scrollview.ScrollToAsync(new_msg, ScrollToPosition.End, false);
        }

        /**
        * This method adds new SEEA message to the stacklayout to be shown on the screen.
        *
        * @param msg which the message that were generated by SEEA.
        */
        private void add_new_seea_msg(String msg) {

            //construct a SEEA chat frame with the correct colors.
            SeeaTalkFrame new_msg = new SeeaTalkFrame(Seea_talk_bg_color, Seea_talk_text_color, Seea_talk_border_color);

            //set the response inside the frame constructed.
            new_msg.Text = msg;

            //Add the frame to the stacklayout.
            chat_messages_stack.Children.Add(new_msg);
            //scroll to the end of the screen to include the added message (frame) in the scope.
            chat_message_scrollview.ScrollToAsync(new_msg, ScrollToPosition.End, false);
        }
    }
}
