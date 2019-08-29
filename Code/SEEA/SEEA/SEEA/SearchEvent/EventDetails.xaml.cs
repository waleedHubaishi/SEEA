using System;
using System.Collections.Generic;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using System.Diagnostics;
using System.Linq;
using System.Globalization;
using Calendar = Plugin.Calendars.Abstractions.Calendar;
using Rg.Plugins.Popup.Services;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;

namespace SEEA
{
    public partial class EventDetails : ContentPage
    {
        public Event previewedEvent = new Event();
        DateTime eventDateAndTime;

        public static String SelectedCalendarName = "";

        static CalendarEvent calendarEvent;


        public EventDetails(Event tappedEvent)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            header.Text = AppResources.EventDetailsHeader;

            //assign the passed event to the event to be previewed
            previewedEvent = tappedEvent;

            bookNowButton.Text = AppResources.bookNowButton;
            eventDiscriptionHeaderLabel.Text = AppResources.eventDiscriptionHeaderLabel;
            costLabel.Text = AppResources.costLabel;
            eventByLabel.Text = AppResources.eventByLabel;

            //set the content of the labels to be displayed
            eventImage.Source = ImageSource.FromFile(previewedEvent.EventImage);
            eventDetailsCost.Text = previewedEvent.EventCost;
            eventDetailsName.Text = previewedEvent.EventName;
            eventDetailsAdress.Text = previewedEvent.EventAddress;
            eventDetailsDescription.Text = previewedEvent.EventDiscription;
            eventDetailsDate.Text = previewedEvent.EventDate.ToString();
            eventDetailsOrg.Text = previewedEvent.EventOrg;
            eventDateAndTime = DateFormatConversion.ConvertStringToDate(previewedEvent.EventDate);

            //set the correct heart picture, if event is saved then full, otherwise empty
            if (previewedEvent.IsSaved == true)
            {
                saveButton.Source = "saveFull.png";
            }
            else
            {
                saveButton.Source = "saveEmpty.png";
            }
        }

        public async void returnToPreviousPage(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }

        public async void returnToHomePage(object sender, EventArgs args)
        {
            await Navigation.PopToRootAsync();
        }


        [Obsolete]
        /**
        * This method opens the address in the platform maps app.
        *
        * @param sender that initiates the action.
        * @param eventArgs of the event itself.
        */
        public void OpenAddressInMap(object sender, EventArgs eventArgs)
        {
            string url = "";

            Device.OnPlatform(iOS: () =>
            {
                url = String.Format("http://maps.apple.com/maps?q={0}", previewedEvent.EventAddress);
            },
              Android: () =>
              {
                  url = String.Format("http://maps.google.com/maps?q={0}", previewedEvent.EventAddress);
              });

            var addressUrl = new UriBuilder(url).Uri;
            Device.OpenUri(addressUrl);
        }

        /**
        * This method books an event when the user clicks on the Book Event button. If event is already booked then dont book it again.
        *
        * @param sender that initiates the action.
        * @param eventArgs of the event itself.
        */
        public async void BookEvent(object sender, EventArgs eventArgs)
        {
            //The event is not yet booked as an initial step.
            bool eventBooked = false;

            //Search the list of the booked events, if an event with the same ID has been found, then dont book it again for the same user.
            foreach (Event e in StaticLists.bookedEventsList) {

                //Previously booked event has been found with the same ID, then set eventBooked to true.
                if (e.EventId == previewedEvent.EventId) {
                    eventBooked = true;
                }
            }

            if (eventBooked) {
                await DisplayAlert("Event already booked", $"The event {previewedEvent.EventName} has been booked already.", "OK");
            }

            //No previously booked event with the same ID has been found, then book it.
            else
            {
                StaticLists.bookedEventsList.Add(previewedEvent);
                await DisplayAlert("Event booked successfully", $"The event {previewedEvent.EventName} has been booked successfully.", "OK");
            }
        }

        /**
        * This method changes the the state of an event being saved, based on the user's tap on the save (heart) button.
        *
        * @param sender that initiates the action.
        * @param eventArgs of the event itself.
        */
        public void SaveEventToMyLocker(object sender, EventArgs eventArgs)
        {
            //Search through the list to update the state of the event tapped.
            for (int i = 0; i < StaticLists.eventList.Count; i++)
            {
                //When the event is found
                if (StaticLists.eventList.ElementAt(i).EventId == previewedEvent.EventId)
                {

                    if (StaticLists.eventList.ElementAt(i).IsSaved == false)
                    {
                        StaticLists.eventList.ElementAt(i).IsSaved = true;

                        //set the correct picture accoarding to the  IsSaved property.
                        saveButton.Source = "saveFull.png";
                    }

                    else
                    {
                        StaticLists.eventList.ElementAt(i).IsSaved = false;

                        //set the correct picture accoarding to the previewedEvent IsSaved current property.
                        saveButton.Source = "saveEmpty.png";
                    }

                    //break because continue searching is unneeded overload as each event exists
                    //only once.
                    break;
                }
            }

            
        }

        /**
        * This method handles the calendar access permissions using the plugin called Plugin.Permissions and then adds the creates the object
        * of type CalendarEvent using the plugin called Plugin.Calendars in order to add it to the chosen calendar.
        *
        * @param sender that initiates the action.
        * @param eventArgs of the event itself.
        */
        public async void ShowPopup(object sender, EventArgs eventArgs)
        {
            int commandParameter = Convert.ToInt32(((Button)sender).CommandParameter);
            Permission permission = (Permission)commandParameter;

            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

            //if the user chosen not to give permission to access the calendar, register the state as not given.
            if (permissionStatus != PermissionStatus.Granted)
            {
                var response = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                var userResponse = response[permission];

                Debug.WriteLine($"Permission {permission} {userResponse}");
            }
            //if permission granted
            else
            {
                Debug.WriteLine($"Permission {permission} {permissionStatus}");

                var calendars = await CrossCalendars.Current.GetCalendarsAsync();

                //creating an object of CalendarEvent and populate it with the data to save it in the selected calendar.
                calendarEvent = new CalendarEvent
                {
                    Name = previewedEvent.EventName,
                    Start = eventDateAndTime,
                    Location = previewedEvent.EventAddress,
                    End = eventDateAndTime.AddHours(1),
                    Reminders = new List<CalendarEventReminder> { new CalendarEventReminder() }
                };

                //this window handles the selection of the calendar to add the event to.
                await PopupNavigation.Instance.PushAsync(new CalendarSelectionPopup(calendars));
            }
        }

        /**
        * This method handles the calendar access permissions using the plugin called Plugin.Permissions and then adds the event to the calendar chosen using.
        * 
        * @param calendarName which is the selected calendar from the popup window.
        */
        public async static void AddEventToChosenCalendar(String calendarName){
            var calendars = await CrossCalendars.Current.GetCalendarsAsync();

            Calendar chosenCalendar = null;
            for (int i = 0; i < calendars.Count; i++)
            {
                if (calendars[i].Name == calendarName) {
                    chosenCalendar = calendars[i];
                    break;
                }
            }

            await CrossCalendars.Current.AddOrUpdateEventAsync(chosenCalendar, calendarEvent);
            await Application.Current.MainPage.DisplayAlert("Confirmation", $"The event has been successfully saved into {chosenCalendar.Name}.","Hide");
        }
    }
}
