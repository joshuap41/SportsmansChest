using System;
using System.Collections.Generic;
using SportsmansChest.Model.SportingLocationsModel;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace SportsmansChest.View.SportingLocationsViews
{
    public partial class LocationDetailsPage : ContentPage
    {
        private Location selectedLocation;

        public LocationDetailsPage(Location selectedLocation)
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

        void DeleteLocation_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
