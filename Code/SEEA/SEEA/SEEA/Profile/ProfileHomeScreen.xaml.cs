using System;
using System.Collections;
using System.Collections.Generic;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;
using Xamarin.Forms;
using System.Diagnostics;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using System.Linq;

namespace SEEA
{
    public partial class ProfileHomeScreen : ContentPage
    {
        ArrayList calendars = new ArrayList();
        public static string USER_NAME = "Mark";

        public ProfileHomeScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            header.Text = AppResources.ProfileHeader;
            welcomeLabel.Text = AppResources.welcomeLabel+" "+USER_NAME;
            personalDataHeaderLabel.Text = AppResources.personalDataHeaderLabel;
            nameLabel.Text = AppResources.nameLabel;
            addressLabel.Text = AppResources.addressLabel;
            emailLabel.Text = AppResources.emailLabel;
            myReservationslabel.Text = AppResources.myReservationslabel;
        }

        public async void returnToPreviousPage(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }

        public async void returnToHomePage(object sender, EventArgs args)
        {
            await Navigation.PopToRootAsync();
        }

        public async void ToReservations(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new BookedEvents());
        }

    }


}
