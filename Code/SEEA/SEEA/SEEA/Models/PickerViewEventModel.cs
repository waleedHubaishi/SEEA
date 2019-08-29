using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SEEA
{
    public class EventType
    {
        public string EventTypeName { get; set; }
        public int EventTypeId { get; set; }

    }

    public class PickerViewEventModel : INotifyPropertyChanged
    {

        public List<EventType> EventList { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;


        public PickerViewEventModel()
        {
            EventList = GetEvents().ToList();
        }



        protected void OnPropertyChanged([CallerMemberName] String name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public EventType _selectedEventType { set; get; }
        public EventType SelectedEventType
        {

            get { return _selectedEventType; }

            set
            {

                if (_selectedEventType != value)
                {

                    _selectedEventType = value;
                    MyEventType = _selectedEventType.EventTypeName;
                }
            }

        }

        public string _myEventType;
        public string MyEventType
        {

            get { return _myEventType; }
            set
            {
                if (_myEventType != value)
                {
                    _myEventType = value;
                    OnPropertyChanged();
                }
            }

        }


        public List<EventType> GetEvents()
        {

            var events = new List<EventType>() {

                new EventType(){EventTypeName="Any", EventTypeId=1},

                new EventType(){EventTypeName="Anlass", EventTypeId=2},
                new EventType(){EventTypeName="Abendveranstaltung", EventTypeId=3},
                new EventType(){EventTypeName="Firmenbesichtigung", EventTypeId=4},
                new EventType(){EventTypeName="Meeting", EventTypeId=5},
                new EventType(){EventTypeName="Seminar", EventTypeId=6},
                new EventType(){EventTypeName="Kongress", EventTypeId=7},
                new EventType(){EventTypeName="Workshop", EventTypeId=8},
                new EventType(){EventTypeName="Versammlung", EventTypeId=9},
                new EventType(){EventTypeName="Kultureller Anlass", EventTypeId=10},


            };

            return events;

        }

    }
}
