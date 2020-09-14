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
            

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();

                // handles Longitude and Latitude input
                //double longitudeOut;
                //bool longParsed = double.TryParse(longitude.Text, out longitudeOut);
                //bool longValid = double.IsNaN(longitudeOut);

                if (string.IsNullOrWhiteSpace(locationName.Text) || string.IsNullOrWhiteSpace(Convert.ToString(eventTypePicker.SelectedItem)) ||
                    string.IsNullOrWhiteSpace(Convert.ToString(notificationStatus.SelectedItem)) || string.IsNullOrWhiteSpace(notes.Text)) 
                {
                    await DisplayAlert("Failure", "Please enter valid information for all location fields including only numbers/decimil for Longitude and Latitude.", "OK");
                }
                else
                {
                    LocationDb newLocation = new LocationDb
                    {
                        CurrentUser = App.UserLoggedIn,
                        LocationName = locationName.Text,
                        Longitude = Convert.ToDouble(longitude.Text),
                        Latitude = Convert.ToDouble(latitude.Text),
                        EventType = Convert.ToString(eventTypePicker.SelectedItem),
                        CreatedDate = DateTime.Now,
                        ReturnDate = returnDate.Date,
                        Notification = Convert.ToString(notificationStatus.SelectedItem),
                        Notes = notes.Text
                    };
                    conn.Insert(newLocation);
                    await DisplayAlert("Success", "Location successfully created", "OK");
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
