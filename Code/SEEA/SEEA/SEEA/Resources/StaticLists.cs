using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using MvvmHelpers;

namespace SEEA
{
    public static class StaticLists
    {
        public static List<int> eventsAddedToCalendar = new List<int>();
        public static ObservableCollection<int> savedEvents = new ObservableCollection<int>();

        public static ObservableCollection<Event> bookedEventsList = new ObservableCollection<Event>();

        public static ObservableCollection<String> userTags = new ObservableCollection<String>();

        public static ObservableCollection<Search> savedSearches = new ObservableCollection<Search> {
        new Search(){  SearchText = "Versammlung", Date = new DateTime(2019, 05, 09), Place = "Zürich", Organization = "Vaud", EventType = "Anlass" }
        };

        public static IList<Event> eventList = new List<Event> {

                new Event(){EventName="Kegelnachmittage 2019 der STV-Senioren",EventPlace="Bern", EventDiscription ="Auskunft und Anmeldung bei Peter Bühler (031 301 01 00; perob@bluewin.ch)",EventCost="0 CHF",
                EventAddress="Restaurant Union, Brunngasse 36, 3011 Bern", EventDate="03.09.2020, 10:00", EventOrg="ETH", EventImage="conference3.jpeg",EventType="Meeting", IsSaved=false, EventId=0, EventTags = new String[] { "senioren" } },

                new Event(){EventName="General Versammlung and round table",EventPlace="Winterthur", EventDiscription = "visit us and you wont be disappointed",EventCost="50 CHF",
                EventAddress="Restaurant Union, Brunngasse 36, 8400 Winterthur", EventDate="11.05.2019, 13:30", EventOrg="ZHAW", EventImage="conference2.jpg",EventType="Anlass", IsSaved=false, EventId=1, EventTags = new String[] { "versammlung","round table" } },

                new Event(){EventName="RoundTable Disccusion",EventPlace="Zürich", EventDiscription = "visit us now",EventCost="40 CHF",
                EventAddress="Wülflingerstrasse 66. 8400 Winterthur", EventDate="11.05.2019, 13:30", EventOrg="FHNW", EventImage="conference1.jpg",EventType="Workshop", IsSaved=false, EventId=2, EventTags = new String[] { "discussion","round table" }}
            };


    }
}
