using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq.Expressions;
using System.Linq;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;

namespace SEEA
{
    public partial class SearchResult : ContentPage
    {
        String searchText = "";
        DateTime? date = null;
        String place = "";
        String org = "";
        String eventType = "";

        FilterPopup popupPage = new FilterPopup();

        public SearchResult(String searchText, DateTime? date, String place, String org, String eventType)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            header.Text = AppResources.SearchResultsHeader;

            searchResultEndOfList.Text = AppResources.searchResultEndOfList;

            //set the criteria which will be used to filter the list of all events.
            this.searchText = searchText;
            this.date = date;
            this.place = place;
            this.org = org;
            this.eventType = eventType;

            eventsTable.ItemsSource = retrieveMatchedList();
        }

        /**
        * This method retrieves the list of events that matches the user entered search criteria if any was given.
        */
        public IList<Event> retrieveMatchedList()
        {
            //create a temp list of event.
            IList<Event> matchedList = StaticLists.eventList.ToList();

            //Search through the list that contains all events.
            for (int y = 0; y < StaticLists.eventList.ToList().Count; y++)
            {
                //if the search text was not empty, then filter using the search text.
                if (searchText != "")
                {
                    for (int i = 0; i < matchedList.Count; i++)
                    {

                        if (matchedList.ElementAt(i).EventName.ToLower().Contains(searchText.ToLower()) == false)
                        {
                            matchedList.RemoveAt(i);
                        }
                    }

                }

                //if the place was not empty, then use the place to filter the results as well.
                if (place != "")
                {
                    for (int i = 0; i < matchedList.Count; i++)
                    {
                        if (matchedList.ElementAt(i).EventPlace.ToLower().Equals(place.ToLower()) == false)
                        {
                            matchedList.RemoveAt(i);
                        }
                    }

                }

                //if the ort was not empty, and the org value selected was not any, use it to filter the results further.
                if ((org != "") && (org.ToLower()!="any"))
                {
                    for (int i = 0; i < matchedList.Count; i++)
                    {
                        if (matchedList.ElementAt(i).EventOrg.ToLower().Equals(org.ToLower()) == false)
                        {
                            matchedList.RemoveAt(i);
                        }
                    }
                }

                //if the event type was not empty, and the value set was not any, then filter using it.
                if ((eventType != "") && (eventType.ToLower() !="any"))
                {
                    for (int i = 0; i < matchedList.Count; i++)
                    {
                        if (matchedList.ElementAt(i).EventType.ToLower().Equals(eventType.ToLower()) == false)
                        {
                            matchedList.RemoveAt(i);
                        }
                    }
                }

                //use the date to filter if the date was not empty.
                if (date != null)
                {
                    for (int i = 0; i < matchedList.Count; i++)
                    {
                        String dateInList = matchedList.ElementAt(i).EventDate.Substring(0,10);

                        string formattedDate = date.Value.ToString("dd.MM.yyyy");

                        if (dateInList.Trim().Equals(formattedDate.Trim()) == false)
                        {
                            matchedList.RemoveAt(i);
                        }
                    }
                }
            }

            //if the filtered list using the previously mentioned criteria was empty, then show the user that there were no results matching.
            if (matchedList.Count == 0)
            {
                emptyListLabelIsVisible.IsVisible = true;
                filterAndListview.IsVisible = false;
            }

            else if (matchedList.Count != 0)
            {
                filterAndListview.IsVisible = true;
                emptyListLabelIsVisible.IsVisible = false;
            }

            //return the filtered list at the end.
            return matchedList;
        }

     
        public async void returnToPreviousPage(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }

        public async void returnToHomePage(object sender, EventArgs args)
        {
            await Navigation.PopToRootAsync();
        }

        //Show the filter extra options pop-up.
        //TODO: use those options to filter the results even more.
        public async void ShowPopup(object sender, EventArgs eventArgs)
        {
            await PopupNavigation.Instance.PushAsync(popupPage);
        }

        /**
        * This method handels tapping on a cell, and navigates the user to the details page.
        */
        public async void cellTapped(object sender, ItemTappedEventArgs e)
        {
            Event tappedEvent = e.Item as Event;

            //await Navigation.PushAsync(new EventDetails(eventTapped.EventName, eventTapped.EventAddress, eventTapped.EventCost, eventTapped.EventDate, eventTapped.EventDiscription, eventTapped.EventPlace, eventTapped.EventOrg, eventTapped.EventImage, eventTapped.EventId, eventTapped.IsSaved, eventTapped.EventType));
            await Navigation.PushAsync(new EventDetails(tappedEvent));

        }


    }
}
