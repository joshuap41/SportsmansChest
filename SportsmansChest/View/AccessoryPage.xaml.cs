using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SportsmansChest.Model;
using SQLite;

namespace SportsmansChest.View
{
    public partial class AccessoryPage : ContentPage
    {
        private Accessory selectedAccessory;

        public AccessoryPage(Accessory selectedAccessory)
        {
            this.selectedAccessory = selectedAccessory;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Description.Text = selectedAccessory.Description;
            Manufacturer.Text = selectedAccessory.Manufacturer;
            Model.Text = selectedAccessory.Model;
            SerialNumnber.Text = selectedAccessory.SerialNumber;
            DeclairedValue.Text = selectedAccessory.DeclairedValue;
            CreatedDate.Text = selectedAccessory.CreatedDate.ToString(App.dateFormat);
            Notes.Text = selectedAccessory.Notes;

            base.OnAppearing();
        }

        async void DeleteAccessory_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();

                var confirmationAccept = await DisplayAlert("Delete", "Delete this accessory?", "Yes", "No");
                if (confirmationAccept)
                {
                    conn.Delete(selectedAccessory);
                    await Navigation.PopAsync();
                }
            }
        }

        async void EditAccessory_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditAccessoryPage(selectedAccessory)));
        }
    }
}
