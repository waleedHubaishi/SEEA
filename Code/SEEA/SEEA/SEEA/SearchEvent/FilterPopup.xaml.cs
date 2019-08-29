using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SEEA.Helpers.Rsources;
using SEEA.Helpers;

namespace SEEA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterPopup : PopupPage
    {
        public FilterPopup()
        {
            InitializeComponent();

            popupHeader.Text = AppResources.popupHeader;
            showResultsWithThoseTagsLabel.Text = AppResources.showResultsWithThoseTagsLabel;
            addSelectedTagsToLikedTagsLabel.Text = AppResources.addSelectedTagsToLikedTagsLabel;
            showMatchesWithMyLikedTagsLabel.Text = AppResources.showMatchesWithMyLikedTagsLabel;
            applyFilterButton.Text = AppResources.applyFilterButton;



        }
    }
}
