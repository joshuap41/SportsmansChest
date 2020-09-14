using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SportsmansChest.Model;
using SQLite;


namespace SportsmansChest.View
{
    public partial class EditAccessoryPage : ContentPage
    {
        private Accessory selectedAccessory;

        public EditAccessoryPage(Accessory selectedAccessory)
        {
            InitializeComponent();

            this.selectedAccessory = selectedAccessory;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            description.Text = selectedAccessory.Description;
            manufacturer.Text = selectedAccessory.Manufacturer;
            model.Text = selectedAccessory.Model;
            serialNumber.Text = selectedAccessory.SerialNumber;
            declairedValue.Text = selectedAccessory.DeclairedValue;
            notes.Text = selectedAccessory.Notes;
        }

        async void CancelToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedAccessory.Description = description.Text;
            selectedAccessory.Manufacturer = manufacturer.Text;
            selectedAccessory.Model = model.Text;
            selectedAccessory.SerialNumber = serialNumber.Text;
            selectedAccessory.DeclairedValue = declairedValue.Text;
            selectedAccessory.Notes = notes.Text;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();

                if (string.IsNullOrWhiteSpace(description.Text) || string.IsNullOrWhiteSpace(manufacturer.Text) ||
                    string.IsNullOrWhiteSpace(model.Text) || string.IsNullOrWhiteSpace(serialNumber.Text) ||
                    string.IsNullOrWhiteSpace(declairedValue.Text))
                {
                    await DisplayAlert("Failure", "Please provide enter information for all accessory fields", "OK");
                }
                else
                {
                    conn.Update(selectedAccessory);
                    await DisplayAlert("Success", "Accessory successfully updated", "OK");
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}
