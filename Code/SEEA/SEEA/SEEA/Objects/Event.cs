using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SEEA
{
    public class Event
    {
        public int EventId { set; get; }
        public String EventName { set; get; }
        public String EventPlace { set; get; }
        public String EventDiscription { set; get; }
        public String EventAddress { set; get; }
        public String EventCost { set; get; }
        public String EventDate { set; get; }
        public String EventOrg { set; get; }
        public String EventImage { set; get; }
        public String EventType { set; get; }
        public bool IsSaved { set; get; }
        public String[] EventTags { set; get; }

    }

    /*public class EventListViewViewModel {
        public ObservableCollection<Event> EventList { get; set; }

        public EventListViewViewModel() {

            EventList = new ObservableCollection<Event>
            {
                 new Event(){EventName="Kegelnachmittage 2019 der STV-Senioren",EventPlace="Bern", EventDiscription ="Auskunft und Anmeldung bei Peter Bühler (031 301 01 00; perob@bluewin.ch)",EventCost="0 CHF",
                EventAddress="Restaurant Union, Brunngasse 36, 3011 Bern", EventDate="03.09.2020, 10:00", EventOrg="ETH", EventImage="conference3.jpeg",EventType="Meeting", IsSaved=false, EventId=0 },

                new Event(){EventName="General Versammlung and round table",EventPlace="Winterthur", EventDiscription = "visit us and you wont be disappointed",EventCost="50 CHF",
                EventAddress="Restaurant Union, Brunngasse 36, 8400 Winterthur", EventDate="11.05.2019, 13:30", EventOrg="ZHAW", EventImage="conference2.jpg",EventType="Anlass", IsSaved=false, EventId=1 },

                new Event(){EventName="RoundTable Disccusion",EventPlace="Zürich", EventDiscription = "visit us now",EventCost="40 CHF",
                EventAddress="Wülflingerstrasse 66. 8400 Winterthur", EventDate="11.05.2019, 13:30", EventOrg="FHNW", EventImage="conference1.jpg",EventType="Workshop", IsSaved=false, EventId=2 }
            };
        }

    }*/
}