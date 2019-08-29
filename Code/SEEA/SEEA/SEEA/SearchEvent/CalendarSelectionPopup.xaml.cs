using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Calendar = Plugin.Calendars.Abstractions.Calendar;
using Rg.Plugins.Popup.Services;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;


namespace SEEA
{
    public partial class CalendarSelectionPopup : PopupPage
    {
        String selectedCalendarName = "";
        public CalendarSelectionPopup(IList<Calendar> calendars)
        {
            InitializeComponent();
            caelndarsAvaliable.ItemsSource = calendars;
            //customCell.SelectedItemBackgroundColor = Color.Gray;
            caelndarsAvaliable.SelectedItem = (caelndarsAvaliable.ItemsSource as IList<Calendar>)[0];
        }

        /**
        * This method handels the tapping on the selected caelndar in the popup view.
        *
        * @param sender the object that initiated the action.
        * @param itemTapped the arguments of the item that was tapped on.
        */
        public void SelectCalendar(object sender, ItemTappedEventArgs itemTapped)
        {
            Calendar selectedCalendar = itemTapped.Item as Calendar;
            selectedCalendarName = selectedCalendar.Name;
        }

        /**
        * This method set the selected calendar from the pop-up view in the EventDetails class.
        *
        * @param sender the object that initiated the action.
        * @param eventArgs the arguments of the item itself.
        */
        public async void ApplyChosenCalendar(object sender, EventArgs eventArgs) {

            //set the calendar to the tapped one.
            EventDetails.SelectedCalendarName = selectedCalendarName;

            //add the event to the selected calendat.
            EventDetails.AddEventToChosenCalendar(selectedCalendarName);


            await PopupNavigation.Instance.PopAsync(true);
        } 
    }
}
