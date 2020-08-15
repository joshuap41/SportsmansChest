using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace SportsmansChest.View.SportingLocationsViews
{
    public partial class AddLocationPage : ContentPage
    {
        private double currentLatitude = 0;
        private double currentLongitude = 0;


        public AddLocationPage()
        {
            InitializeComponent();
        }

        //Geolocation
        //https://docs.microsoft.com/en-us/xamarin/essentials/geolocation?tabs=android
        async protected override void OnAppearing()
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
            Longitude.Text = currentLongitude.ToString();
            Latitude.Text = currentLatitude.ToString();

            base.OnAppearing();
        }

        void Cancel_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void Save_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        
    }
}
