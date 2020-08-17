using System;
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

        }
        async void Cancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void Save_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
