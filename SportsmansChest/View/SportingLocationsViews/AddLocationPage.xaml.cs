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
        private double currentLatitude = 0;
        private double currentLongitude = 0;

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
            //LocationDb loc = new LocationDb();

            //loc.LocationName = locationN.Text;
            //loc.Longitude = Convert.ToDouble(longitude.Text);
            //loc.Latitude = Convert.ToDouble(latitude.Text);

            //// a selected item must be done like this or an exception will occur
            //loc.EventType = Convert.ToString(eventTypePicker.SelectedItem);
            //loc.CreatedDate = DateTime.Now;
            //loc.ReturnDate = returnDate.Date;
            //loc.Notification = Convert.ToString(notificationStatus.SelectedItem);
            //loc.Notes = notes.Text;

            // not set to an instance of an object...... exception?
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
                conn.Insert(newLocation);
                //validate user input
                await Navigation.PopModalAsync();
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
                    location.Latitude = this.currentLatitude;
                    location.Longitude = this.currentLongitude;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Something is wrong: {ex.Message}");
            }
            longitude.Text = currentLongitude.ToString();
            latitude.Text = currentLatitude.ToString();
        }
    }
}
