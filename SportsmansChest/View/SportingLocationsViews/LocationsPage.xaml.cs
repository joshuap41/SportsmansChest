using System;
using System.Collections.Generic;
using SportsmansChest.Model;
using SQLite;
using System.Linq;
using SportsmansChest.Model.SportingLocationsModel;

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
                conn.CreateTable<Location>();
                var locations = conn.Table<Location>().ToList();
                LocationListView.ItemsSource = locations;
            }
        }

        async void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddLocationPage());
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Location>();
                var location = conn.Table<Location>().ToList();

                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    LocationListView.ItemsSource = location;
                }
                else
                {
                    var foundLocation = (from Location in location
                                         where Location.LocationName.ToUpper().StartsWith(e.NewTextValue.ToUpper())
                                         select Location).ToList();
                    LocationListView.ItemsSource = foundLocation;
                }
            }
        }

        void LocationListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedLocation = LocationListView.SelectedItem as Location;

            if (selectedLocation != null)
                Navigation.PushAsync(new LocationDetailsPage(selectedLocation));
        }
    }
}