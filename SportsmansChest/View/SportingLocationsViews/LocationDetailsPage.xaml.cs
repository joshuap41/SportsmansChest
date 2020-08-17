﻿using System;
using System.Collections.Generic;
using SportsmansChest.Model;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace SportsmansChest.View.SportingLocationsViews
{
    public partial class LocationDetailsPage : ContentPage
    {
        private SportLocationDb selectedLocation;

        public LocationDetailsPage(SportLocationDb selectedLocation)
        {
            this.selectedLocation = selectedLocation;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LocationName.Text = selectedLocation.LocationName;
            Longitude.Text = selectedLocation.Longitude.ToString();
            Latitude.Text = selectedLocation.Latitude.ToString();
            EventType.Text = selectedLocation.EventType;
            CreatedDate.Text = selectedLocation.CreatedDate.ToString(App.dateFormat);
            ReturnDate.Text = selectedLocation.ReturnDate.ToString(App.dateFormat);
            Notification.Text = selectedLocation.Notification;
            Notes.Text = selectedLocation.Notes;

            base.OnAppearing();
        }

        void EditLocation_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void DeleteLocation_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<SportLocationDb>();

                var confirmationAccept = await DisplayAlert("Delete", "Delete this item?", "Yes", "No");
                if (confirmationAccept)
                {
                    conn.Delete(selectedLocation);
                    await Navigation.PopAsync();
                }
                else
                {

                }
            }
        }

        void Directions_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
