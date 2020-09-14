using System;
using System.Collections.Generic;
using SportsmansChest.Model;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace SportsmansChest.View.SportingLocationsViews
{
    public partial class LocationsPage : ContentPage
    {

        public LocationsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();
                var locations = conn.Table<LocationDb>().ToList();

                var userLocations = (from LocationDb in locations
                                 where LocationDb.CurrentUser == App.UserLoggedIn
                                 orderby LocationDb.LocationName
                                 select LocationDb).ToList();

                LocationListView.ItemsSource = userLocations;
            }
        }

        async void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddLocationPage()));
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();
                var location = conn.Table<LocationDb>().ToList();

                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    var defaultLocations = (from Location in location
                                            where Location.CurrentUser == App.UserLoggedIn
                                            orderby Location.LocationName
                                            select Location).ToList();

                    LocationListView.ItemsSource = defaultLocations;
                }
                else
                {
                    var foundLocation = (from Location in location
                                         where (Location.LocationName.ToUpper().StartsWith(e.NewTextValue.ToUpper())) &&
                                         (Location.CurrentUser == App.UserLoggedIn)
                                         select Location).ToList();

                    LocationListView.ItemsSource = foundLocation;
                }
            }
        }

        void LocationListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedLocation = LocationListView.SelectedItem as LocationDb;

            if (selectedLocation != null)
                Navigation.PushAsync(new LocationDetailsPage(selectedLocation));
        }
    }
}