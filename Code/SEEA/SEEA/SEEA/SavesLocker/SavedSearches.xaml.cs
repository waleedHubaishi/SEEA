using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEEA.Helpers.Rsources;
using Xamarin.Forms;

namespace SEEA
{
    public partial class SavedSearches : ContentPage
    {

        public SavedSearches()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            header.Text = AppResources.SavedSearchesHeader;
            savedSearchesEndOfList.Text = AppResources.savedSearchesEndOfList;
            ToSearchEventButton.Text = AppResources.SearchEventButtonLabelText;
            warningFirstLabel.Text = AppResources.NoSearchesFirstLine;
            warningSecondLabel.Text = AppResources.NoSearchesSecondLine;


        }

        /**
        * This method handels the tapping on the delete image inside the cell.
        */
        private void DeleteSearch(object sender, EventArgs e)
        {

            var image = sender as Image;

            var searchToRemove = image?.BindingContext as Search;

            //execute the command only on the cell that its image was tapped on.
            RemoveSearchCommand.Execute(searchToRemove);

            //refresh the content of the table.
            decideAppearingViews();
        }

        /**
        * This Command removes the tapped on search from the list of saved searches.
        */
        public Command<Search> RemoveSearchCommand
        {
            get
            {
                return new Command<Search>(
                    (Search search) => {
                        StaticLists.savedSearches.Remove(search);
                    });
            }
        }

        /**
        * This method is responsible of deciding which view shall appear, either the table of saved searches or the warning label indication that there are
        * no saves searches.
        */
        public void decideAppearingViews()
        {
            if (StaticLists.savedSearches.Count == 0)
            {
                searchesTable.IsVisible = false;
                emptyListLabelIsVisible.IsVisible = true;
            }

            else
            {
                emptyListLabelIsVisible.IsVisible = false;
                searchesTable.IsVisible = true;
            }
        }

        /**
        * This method is responsible of navigating the user to the search results of the search criteria tapped on. 
        */
        public async void ToSearchResults(object sender, ItemTappedEventArgs eventArgs)
        {
            Search eventTapped = eventArgs.Item as Search;
            await Navigation.PushAsync(new SearchResult(eventTapped.SearchText,
                                        eventTapped.Date, eventTapped.Place,
                                        eventTapped.Organization, eventTapped.EventType));
        }


        protected override void OnAppearing()
        {
            decideAppearingViews();
            searchesTable.ItemsSource = StaticLists.savedSearches;
        }

        private async void To_Search_Event(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchEventScreen());
        }

        public async void returnToPreviousPage(object sender, EventArgs args)
        {
            await Navigation.PopAsync();
        }

        public async void returnToHomePage(object sender, EventArgs args)
        {
            await Navigation.PopToRootAsync();
        }

    }
}
