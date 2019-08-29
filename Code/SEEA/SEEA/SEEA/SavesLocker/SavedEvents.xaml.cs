using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq.Expressions;
using System.Linq;
using SEEA.Helpers.Rsources;

namespace SEEA
{
    public partial class SavedEvents : ContentPage
    {

        IList<Event> likedEventsList;

        public SavedEvents()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            header.Text = AppResources.SavedEventsHeader;
            savedEventsEndOfList.Text = AppResources.savedEventsEndOfList;
            ToSearchEventButton.Text = AppResources.SearchEventButtonLabelText;
            warningFirstLabel.Text = AppResources.NoEventsFirstLine;
            warningSecondLabel.Text = AppResources.NoEventsSecondLine;

            //likedEventsList = createListOfLikedEvents(StaticLists.eventList).ToList<Event>();
            //eventsTable.ItemsSource = likedEventsList;

        }

        /**
       * This method creates the list of saved events by itetrating inside the list of all events and check if their IsSaved property is set to true to add
       * it to the temporary list of saved events.
       */
        public static List<Event> CreateListOfLikedEvents(IList<Event> listOfAllEvents)
        {
            List<Event> matchedList = new List<Event>();

            //iterate through the list of all avaliable events, and add the event with IsSaved property is true to a temp matchedList.
            foreach (Event e in listOfAllEvents)
            {

                if (e.IsSaved == true)
                {
                    matchedList.Add(e);
                }
            }
            return matchedList.ToList();
        }


       /**
       * This method is responsible of deciding which view - either the label or the table of saved events - to show
       * based on the number of elements inside the likedEventsList.
       */
        protected override void OnAppearing()
        {
            likedEventsList = CreateListOfLikedEvents(StaticLists.eventList).ToList<Event>();

            //if no liked events were found, then show the warning label and hide the table.
            if (likedEventsList.Count == 0)
            {
                emptyListLabelIsVisible.IsVisible = true;
                eventsTable.IsVisible = false;
            }

            //otherwise show the table of liked events and hide the warning label.
            else if (likedEventsList.Count != 0)
            {

                eventsTable.IsVisible = true;
                emptyListLabelIsVisible.IsVisible = false;
            }
            eventsTable.ItemsSource = likedEventsList;
        }

        /**
        * This method navigates the user to the event tapped details page.
        */
        public async void cellTapped(object sender, ItemTappedEventArgs e)
        {

            Event tappedEvent = e.Item as Event;

            //await Navigation.PushAsync(new EventDetails(eventTapped.EventName, eventTapped.EventAddress, eventTapped.EventCost, eventTapped.EventDate, eventTapped.EventDiscription, eventTapped.EventPlace, eventTapped.EventOrg, eventTapped.EventImage, eventTapped.EventId, eventTapped.IsSaved, eventTapped.EventType));
            await Navigation.PushAsync(new EventDetails(tappedEvent));
        }

        public async void returnToPreviousPage(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }

        public async void returnToHomePage(object sender, EventArgs args)
        {
            await Navigation.PopToRootAsync();
        }

        private async void To_Search_Event(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchEventScreen());
        }

    }
}
