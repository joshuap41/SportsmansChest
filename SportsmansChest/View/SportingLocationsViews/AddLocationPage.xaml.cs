using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Essentials;
using SportsmansChest.Model;
using SportsmansChest.View.SportingLocationsViews;
using SQLite;

namespace SportsmansChest.View.SportingLocationsViews
{
    public partial class AddLocationPage : ContentPage
    {
        public AddLocationPage()
        {
            InitializeComponent();
            GetUserGPSLocation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void Cancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Save_Clicked(System.Object sender, System.EventArgs e)
        {
            LocationDb newLocation = new LocationDb
            {
                LocationName = locationName.Text,
                Longitude = Convert.ToDouble(longitude.Text),
                Latitude = Convert.ToDouble(latitude.Text),
                EventType = Convert.ToString(eventTypePicker.SelectedItem),
                CreatedDate = DateTime.Now,
                ReturnDate = returnDate.Date,
                Notification = Convert.ToString(notificationStatus.SelectedItem),
                Notes = notes.Text
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();

                if (string.IsNullOrWhiteSpace(locationName.Text) || string.IsNullOrWhiteSpace(longitude.Text) ||
                    string.IsNullOrWhiteSpace(latitude.Text) || string.IsNullOrWhiteSpace(Convert.ToString(eventTypePicker.SelectedItem)) ||
                    string.IsNullOrWhiteSpace(Convert.ToString(notificationStatus.SelectedItem)) || string.IsNullOrWhiteSpace(notes.Text))
                {
                    await DisplayAlert("Failure", "Please enter information for all location fields", "OK");
                }
                else
                {
                    conn.Insert(newLocation);
                    await DisplayAlert("Success", "Item successfully created", "OK");
                    await Navigation.PopModalAsync();
                }
            }
        }

        //Geolocation
        //https://docs.microsoft.com/en-us/xamarin/essentials/geolocation?tabs=android
        async public void GetUserGPSLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location == null)
                {
                    await DisplayAlert("GPS Error", "No GPS signal available", "Ok");
                }
                else
                {
                    longitude.Text = location.Longitude.ToString();
                    latitude.Text = location.Latitude.ToString();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
            }
        }
    }
}
