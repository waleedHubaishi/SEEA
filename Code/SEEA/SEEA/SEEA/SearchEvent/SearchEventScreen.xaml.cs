using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;


namespace SEEA
{

    [DesignTimeVisible(true)]
    public partial class SearchEventScreen : ContentPage
    {
        //Because searching for events in five years is not allowed.
        private int THE_YEAR_IN_FIVE_YEARS = DateTime.Now.Year + 5;


        private String textToSearch = "";
        private String place = "";
        private String organization = "";
        private String eventType = "";
        private bool isSearchSaved = false;
        private DateTime? dateEntered = null;

        private PickerViewOrganizationModel pickerViewOrganization = new PickerViewOrganizationModel();
        private PickerViewEventModel pickerViewEvent = new PickerViewEventModel();


        public SearchEventScreen()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            InitializeComponent();
            header.Text = AppResources.SearchEventsHeader;

            //Populate the labels with text from the translation files.
            searchTextLabel.Text = AppResources.searchTextLabel;
            dateLabel.Text = AppResources.dateLabel;
            placeLabel.Text = AppResources.placeLabel;
            organizationLabel.Text = AppResources.organizationLabel;
            eventTypeLabel.Text = AppResources.eventTypeLabel;
            saveSearchLabel.Text = AppResources.saveSearchLabel;
            submitSearchButton.Text = AppResources.submitSearchButton;

            organizationEntered.BindingContext = pickerViewOrganization;
            eventTypeEntered.BindingContext = pickerViewEvent;

        }

        /**
        * This method handels the toggle switch to save the search.
        *
        * @param sender that initiates the action.
        * @param toggledEventArgs of the toggle switch itself.
        */
        private void SaveSearch(object sender, ToggledEventArgs toggledEventArgs)
        {
            //put the current value of the toggle in the boolean variable.
            isSearchSaved = toggledEventArgs.Value;
        }

        public async void returnToHomePage(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }


        public async void returnToPreviousPage(object sender, EventArgs args)
        {
            await Navigation.PopToRootAsync();
        }

        /**
        * This method descides whether to navigate the user to the next screen or not, it handels the Search button.
        *
        * @param sender that initiates the action.
        * @param eventArgs of the event itself.
        */
        public async void ToResults(object sender, EventArgs eventArgs)
        {

            textToSearch = searchTextEntered.Text ?? "";

            place = placeEntered.Text ?? "";

            eventType = pickerViewEvent._myEventType ?? "";

            //if the date entered can be converted into DateTime object, then convert it.
            try
            {
                dateEntered = ConstrcutDateFromString(dateEnteredByUser.Text);
            }
            catch {
                //else set the value to null.
                dateEntered = null;
            }

            organization = pickerViewOrganization.MyOrganization ?? "";


            //if the date null and the value entered by user was empty or white spacce, or if the date is not null, then proceed
            // if ((IsDateAccepted(dateEnteredByUser.Text.Split('.'))) || (String.IsNullOrEmpty(dateEnteredByUser.Text)))
            if (((dateEntered == null) && (String.IsNullOrWhiteSpace(dateEnteredByUser.Text))) || (dateEntered != null))
            {
                //save the search if the user chosed to.
                if (isSearchSaved)
                {
                    if ((String.IsNullOrWhiteSpace(textToSearch)) && (dateEntered == null) && (String.IsNullOrWhiteSpace(place)) && (String.IsNullOrWhiteSpace(organization)) && (String.IsNullOrWhiteSpace(eventType)))
                    {
                        await DisplayAlert(AppResources.searchNotSavedAlertHeader, AppResources.searchNotSavedAlertBody, "OK");
                    }
                    else
                    {
                        StaticLists.savedSearches.Add(new Search() { SearchText = textToSearch, Date = dateEntered, Place = place, Organization = organization, EventType = eventType });
                        await Navigation.PushAsync(new SearchResult(textToSearch, dateEntered, place, organization, eventType));

                        //clear the variables, so that if the user returned, the text fields and selection are empty
                        ClearVariables();
                    }
                }

                //Search not saved.
                else
                {
                    await Navigation.PushAsync(new SearchResult(textToSearch, dateEntered, place, organization, eventType));

                    //clear the variables, so that if the user returned, the text fields and selections are empty
                    ClearVariables();
                }
            }
            //if the top if condition not fullfiled, then show an error message
            else {
                dateEnteredByUser.TextColor = Color.Red;
                //await DisplayAlert("Date error", $"The date {dateEnteredByUser.Text} is not of the format dd.mm.yyyy or is in the past, please correct it before proceeding", "ok");
            }
        }

        /**
        * This method clears the values inside the variables textToSearch, dateEntered, place, organization and eventType.
        */
        public void ClearVariables() {
            textToSearch = "";
            dateEntered = null;
            place = "";
            organization = "";
            eventType = "";
            isSearchSaved = false;
        }

        /**
        * This method constructs a DateTime object from the string.
        *
        * @param dateAsString to be converted into a DateTime object.
        * @return date of type DateTime.
        */
        public DateTime ConstrcutDateFromString(String dateAsString)
        {
                //create an array with three indexes, containing day, month and year.
                String[] dateAsArray = dateAsString.Split('.');

                //if the date entered follows the rules set, construct the date object from the given string.
                if (IsDateAccepted(dateAsArray))
                {
                    return new DateTime(Convert.ToInt32(dateAsArray[2]), Convert.ToInt32(dateAsArray[1]), Convert.ToInt32(dateAsArray[0]));
                }

            throw new FormatException("The date can not be converted");
        }

        /**
        * This method checks if the date follows the rules by checking 4 criterias.
        *
        * @param dateGiven as an array containing the 3 indexes of day, month and year.
        * @return boolean depends on if the date follows the 4 rules checked.
        */
        private bool IsDateAccepted(String[] dateGiven)
        {
            /* if (IsSizeOfDate(dateGiven))
            {
                if (IsDateJustNumbers(dateGiven))
                {
                    if (IsDateLogicallyCorrect(dateGiven))
                    {
                        if (IsDateInTheFuture(dateGiven))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;*/
            if (IsSizeOfDate(dateGiven) && IsDateJustNumbers(dateGiven)
                && IsDateLogicallyCorrect(dateGiven) && IsDateInTheFuture(dateGiven))
            {         
                            return true;
            }
            return false;
        }

        /**
        * This method prints the correct error message for the date entered if any.
        *
        * @param errorType an integer indicating the error type.
        */
        public async void PrintErrorMessage(int errorType)
        {
            switch (errorType)
            {
                case 1:
                    await DisplayAlert(AppResources.dateErrorHeader, AppResources.dateErrorWrongFormatBody, "OK");
                    break;
                case 2:
                    await DisplayAlert(AppResources.dateErrorHeader, AppResources.dateErrorInPastBody, "OK");
                    break;
                case 3:
                    await DisplayAlert(AppResources.dateErrorHeader, AppResources.dateErrorHasCharactersBody, "OK");
                    break;
                case 4:
                    await DisplayAlert(AppResources.dateErrorHeader, AppResources.dateErrorMoreThanFiveYearsBody, "OK");
                    break;
                default:
                    await DisplayAlert(AppResources.dateErrorHeader, AppResources.dateErrorGeneralBody, "OK");
                    break;
            }
        }

        /**
        * This method checks if the month is between 1 and 12, and if the day is between 1 and 31, also if the year is not in more than 5 years.
        *
        * @param dateGiven as an array containing the 3 indexes of day, month and year.
        * @return boolean depends on if the day and month and year follow the rules.
        */
        private bool IsDateLogicallyCorrect(String[] dateGiven)
        {
            int day = 0;
            int month = 0;
            int year = 0;

            if (Int32.TryParse(dateGiven[0], out day))
            {
                if ((day >= 1) && (day <= 31))
                {
                    if (Int32.TryParse(dateGiven[1], out month))
                    {
                        if ((month >= 1) && (month <= 12))
                        {
                            if (Int32.TryParse(dateGiven[2], out year))
                            {
                                if (year < THE_YEAR_IN_FIVE_YEARS)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            PrintErrorMessage(4);
            return false;
        }

        /**
        * This method checks if the date is in the future, which is a condition to show results.
        *
        * @param dateAsArray as an array containing the 3 indexes of day, month and year.
        * @return boolean depends on if the date follow the rules checked.
        */
        public bool IsDateInTheFuture(String[] dateAsArray)
        {
            int givenYear = Convert.ToInt32(dateAsArray[2]);

            //construct a date object.
            DateTime dateGiven = new DateTime(Convert.ToInt32(dateAsArray[2]), Convert.ToInt32(dateAsArray[1]), Convert.ToInt32(dateAsArray[0]));

            //if the date is in the past, also refuse.
            if (dateGiven < DateTime.Now.Date)
            {
                PrintErrorMessage(2);
                return false;
            }

            //else, accept it.
            return true;
        }

        /**
        * This method checks if the date contains characters.
        *
        * @param dateAsArray as an array containing the 3 indexes of day, month and year.
        * @return boolean depends on if the date follow the rules checked.
        */
        public bool IsDateJustNumbers(String[] dateAsArray)
        {

            foreach (String s in dateAsArray)
            {
                //if the date contains a letter, then return false.
                if (s.Any(char.IsLetter))
                {
                    PrintErrorMessage(3);
                    return false;
                }
            }

            return true;
        }


        /**
        * This method checks if the array of string size fits the expected size of a date.
        * Day = 2, Month = 2, Year = 4
        *
        * @param dateAsArray as an array containing the 3 indexes of day, month and year.
        * @return boolean depends on if the date follow the rules checked.
        */
        private bool IsSizeOfDate(String[] dateAsArray)
        {
            //if the date entered does not contain three splitted by "." indexes, return false.
            if (dateAsArray.Length != 3)
            {
                PrintErrorMessage(1);
                return false;
            }

            else
            {
                //if the day does not consist of two digits, then return false.
                if (dateAsArray[0].Length != 2)
                {
                    PrintErrorMessage(1);
                    return false;
                }

                //if the month does not consist of two digits, then return false.
                if (dateAsArray[1].Length != 2)
                {
                    PrintErrorMessage(1);
                    return false;
                }

                //if the year does not consist of four digits, then return false.
                if (dateAsArray[2].Length != 4)
                {
                    PrintErrorMessage(1);
                    return false;
                }
            }
            return true;
        }

    }
}
