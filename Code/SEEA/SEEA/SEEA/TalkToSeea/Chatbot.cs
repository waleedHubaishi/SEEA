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
    public class Chatbot
    {
        public Chatbot()
        {
        }

        /**
       * This method creates SEEA response by combining the tag name with the confirmation message
       * using the chosen langauge on the device.
       *
       * @param tagName which is the tag in a string format.
       * @return seeaResponse which is response and the tag name after combining both.
       */
        public String CreateSEEAResponse(string userInput)
        {

            String seeaResponse = "";

            if ((userInput.ToLower().Contains("like")) || (userInput.ToLower().Contains("love")) || (userInput.ToLower().Contains("interested in")))
            {
                if (ExtractTagFromUserInput(userInput) != "") {
                    seeaResponse = ExtractTagFromUserInput(userInput) + " " + AppResources.tagCaptured;
                }
            }

            else if ((userInput.ToLower().Contains("what is your name")) || (userInput.ToLower().Contains("what's your name"))) {
                seeaResponse = AppResources.myNameIs + " SEEA";
            }

            else if ((userInput.ToLower().Contains("how are you")) || (userInput.ToLower().Contains("how are you doing")) || (userInput.ToLower().Contains("are you ok")))
            {
                seeaResponse = AppResources.iAmFine;

            }

            else if ((userInput.ToLower().Contains("how are doing")) || (userInput.ToLower().Contains("how you doin")))
            {
                seeaResponse = AppResources.iAmFine + ", Mr. Joey Tribbiani";
            }

            else if ((userInput.ToLower().Contains("how many events do i have")) || (userInput.ToLower().Contains("how many events did i book")) || (userInput.ToLower().Contains("how many have i booked")))
            {
                seeaResponse = AppResources.youHave + " " + StaticLists.bookedEventsList.Count;
                if (StaticLists.bookedEventsList.Count == 1)
                {
                    seeaResponse += ($" {AppResources.oneEvent}");
                }
                else {
                    seeaResponse += ($" {AppResources.events}");
                }
            }


            else
            {
                seeaResponse = AppResources.noTagCaptured;
            }

            return seeaResponse;

        }

        /**
        * This method extract the tag from the sent text, the way it works is that it captures the word(s) that come after the
        * specified sentences, such as "like", "love" and "interested in".
        *
        * @param userInput in a textual format.
        * @return tag which is the tag itself as a string.
        */
        private String ExtractTagFromUserInput(string userInput)
        {
            string tag = "";
            if (userInput.ToLower().Contains("like"))
            {
                tag = userInput.After("like");
            }

            else if (userInput.ToLower().Contains("love"))
            {
                tag = userInput.After("love");
            }

            else if (userInput.ToLower().Contains("interested in"))
            {
                tag = userInput.After("interested in");
            }

            StaticLists.userTags.Add(tag);
            return tag;
        }
    }

    
}
