using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Xamarin.Forms;

namespace SEEA.GetInspired
{
    public partial class GetInspiredScreen : ContentPage
    {
        //private HashSet<string> tagsInEvents = new HashSet<string>();
        private IDictionary<String, ArrayList> suggestedEventsForTagsDict = new Dictionary<String, ArrayList>();

        public GetInspiredScreen()
        {
            InitializeComponent();
            //tagsInEvents = GatherTagsFromAllEvents();
            FillUsertagsWithCorrespondingEventsList();
        }

        protected override void OnAppearing(){
            if (suggestedEventsForTagsDict.Count != 0) {

            }
        }

       /**
       * This method goes through the list of all tags entered by the user and
       * calls a function to create multiple records of tags and their corresponding events.
       *
       */
        public void FillUsertagsWithCorrespondingEventsList() {
            foreach (string tag in StaticLists.userTags) {
                CreateTagAndEventsListPair(tag);
            }
        }

       /**
       * This method adds a record inside the Dictionary, which contains a tag as key and
       * an arraylist of events as value.
       *
       * @param tag to be searched for in string format.
       */
        public void CreateTagAndEventsListPair(string tag) {
            foreach (Event e in StaticLists.eventList)
            {
                if (e.EventTags.Contains(tag.ToLower()))
                {
                    suggestedEventsForTagsDict.Add(tag, CreateTagsCorrespondingEventsList(tag));
                }
            }
        }

        /**
        * This method creates a list of all events that contains a given tag.
        *
        * @param tag to be searched for in string format.
        * @return eventsOfTag the list of events that has the tag given.
        */
        public ArrayList CreateTagsCorrespondingEventsList(String tag)
        {
            ArrayList eventsOfTag = new ArrayList();

            //add the event to the list if the tag given exists in the events
            //tags list.
            foreach (Event e in StaticLists.eventList)
            {
                if (e.EventTags.Contains(tag.ToLower()))
                {
                    eventsOfTag.Add(e);
                }
            }
            return eventsOfTag;
        }

        /**
        * This method return the list of all tags exist in the events list.
        *
        * @return allTagsFromEvents which is a set that contains all the tags in events.
        */
        public HashSet<string> GatherTagsFromAllEvents()
        {

            HashSet<string> allTagsFromEvents = new HashSet<string>();

            foreach (Event e in StaticLists.eventList)
            {
                foreach (string tag in e.EventTags)
                {
                    allTagsFromEvents.Add(tag.ToLower());
                }
            }

            return allTagsFromEvents;
        }

        
    }
}
