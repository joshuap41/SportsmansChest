using System;
using System.ComponentModel;

using Xamarin.Forms;
using SportsmansChest.View;
using SportsmansChest.View.SportingLocationsViews;
using SportsmansChest.Model;
using SQLite;

namespace SportsmansChest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void InventoryList_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage());
        }

        async void SportingLocations_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LocationsPage());
        }

        async void ReportButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ReportPage()));
        }
    }
}
