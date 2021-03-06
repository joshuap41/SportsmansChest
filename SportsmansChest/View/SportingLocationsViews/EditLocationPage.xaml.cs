﻿using System;
using System.Collections.Generic;
using SportsmansChest.Model;
using SQLite;

using Xamarin.Forms;

namespace SportsmansChest.View.SportingLocationsViews
{
    public partial class EditLocationPage : ContentPage
    {
        private LocationDb selectedLocation;

        public EditLocationPage(LocationDb selectedLocation)
        {
            InitializeComponent();
            this.selectedLocation = selectedLocation;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            locationName.Text = selectedLocation.LocationName;
            longitude.Text = Convert.ToString(selectedLocation.Longitude);
            latitude.Text = Convert.ToString(selectedLocation.Latitude);
            eventTypePicker.SelectedItem = selectedLocation.EventType;
            returnDate.Date = selectedLocation.ReturnDate;
            notificationStatus.SelectedItem = Convert.ToString(selectedLocation.Notification);
            notes.Text = selectedLocation.Notes;
        }

        async void Cancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Save_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedLocation.LocationName = locationName.Text;
            selectedLocation.Longitude = Convert.ToDouble(longitude.Text);
            selectedLocation.Latitude = Convert.ToDouble(latitude.Text);
            selectedLocation.EventType = Convert.ToString(eventTypePicker.SelectedItem);
            selectedLocation.ReturnDate = returnDate.Date;
            selectedLocation.Notification = Convert.ToString(notificationStatus.SelectedItem);
            selectedLocation.Notes = notes.Text;

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
                    conn.Update(selectedLocation);
                    await DisplayAlert("Success", "Item successfully updated", "OK");
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}
