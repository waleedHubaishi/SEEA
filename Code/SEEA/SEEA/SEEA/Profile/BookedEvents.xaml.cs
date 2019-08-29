using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MvvmHelpers;
using Xamarin.Forms;
using System.Linq;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;

namespace SEEA
{
    public partial class BookedEvents : ContentPage
    {
        public ObservableCollection<Grouping<String, Event>> EventsBooked { get; set; } = new ObservableCollection<Grouping<String, Event>>();
       

         public BookedEvents()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            header.Text = AppResources.BookedEventsHeader;
            reservedEventsEndOfList.Text = AppResources.reservedEventsEndOfList;
            ToSearchEventButton.Text = AppResources.SearchEventButtonLabelText;
            warningFirstLabel.Text = AppResources.NoBookedEventsFirstLine;
            warningSecondLabel.Text = AppResources.NoBookedEventsSecondLine;

            //this LINQ helps to combine the events booked based on their date.
            var items = from eventAvaliable in StaticLists.bookedEventsList
                        orderby eventAvaliable.EventDate.ToString()
                        group eventAvaliable by eventAvaliable.EventDate.Substring(0,10) into eventsGroup
                        select new Grouping<String, Event>(eventsGroup.Key.ToString(), eventsGroup);

            foreach (var g in items) {
                EventsBooked.Add(g); 
            }

            BindingContext = this;
            DecideAppearingViews();
        }

        /**
        * This method responsible of showing either the table of the events booked, or a message indicating that no events were booked.
        */
        public void DecideAppearingViews()
        {
            //hide the table view and show the warning label if no events booked were found
            if (StaticLists.bookedEventsList.Count == 0)
            {
                eventsTable.IsVisible = false;
                emptyListLabelIsVisible.IsVisible = true;
            }

            //if events were found, then show the table and hide the warning label.
            else
            {
                emptyListLabelIsVisible.IsVisible = false;
                eventsTable.IsVisible = true;
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


        public async void ToEventDetails(object sender, ItemTappedEventArgs e)
        {

            Event tappedEvent = e.Item as Event;

            //await Navigation.PushAsync(new EventDetails(eventTapped.EventName, eventTapped.EventAddress, eventTapped.EventCost, eventTapped.EventDate, eventTapped.EventDiscription, eventTapped.EventPlace, eventTapped.EventOrg, eventTapped.EventImage, eventTapped.EventId, eventTapped.IsSaved, eventTapped.EventType));
            await Navigation.PushAsync(new EventDetails(tappedEvent));

        }

        private async void To_Search_Event(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchEventScreen());
        }

    }
}
