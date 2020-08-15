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
            InitializeComponent();

            this.selectedLocation = selectedLocation;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
