using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SEEA.TalkToSeea;
using Xamarin.Essentials;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;



namespace SEEA
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            talkToSeeaButton.Text = AppResources.TalkToSeeaButtonLabelText;
            seachEventsButton.Text = AppResources.SearchEventButtonLabelText;
            getInspiredButton.Text = AppResources.GetInspiredButtonLabelText;
            profileButton.Text = AppResources.ProfileButtonLabelText;
            savesLockerSeeaButton.Text = AppResources.SavesLockerButtonLabelText;


        }

        private async void To_Seea(object sender, EventArgs e) {
            await Navigation.PushAsync(new TalkToSeeaScreen()); 
        }

        private void To_Get_Inspired(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new GetInspiredScreen());
        }

        private async void To_Search_Event(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchEventScreen());
        }


        private async void To_Profile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfileHomeScreen());
        }

        private async void To_Saves_Locker(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SavesLockerScreen());
        }

    }
}
