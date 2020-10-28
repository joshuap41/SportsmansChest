using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SportsmansChest.Model;
using SQLite;
using System.Linq;

namespace SportsmansChest.View
{
    public partial class AddAccessoryPage : ContentPage
    {
        private InventoryItem selectedInventoryItem;

        public AddAccessoryPage(InventoryItem selectedInventoryItem)
        {
            InitializeComponent();

            this.selectedInventoryItem = selectedInventoryItem;
        }

        async void CancelToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            Accessory newAccessory = new Accessory
            {
                InvItem = selectedInventoryItem.Id,
                Description = selectedInventoryItem.Description,
                Manufacturer = manufacturer.Text,
                Model = model.Text,
                SerialNumber = serialNumber.Text,
                DeclaredValue = Convert.ToDouble(declaredValue.Text),
                CreatedDate = DateTime.Now,
                Notes = notes.Text
            };

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();

                if (string.IsNullOrWhiteSpace(description.Text) || string.IsNullOrWhiteSpace(manufacturer.Text) ||
                    string.IsNullOrWhiteSpace(model.Text) || string.IsNullOrWhiteSpace(serialNumber.Text) ||
                    declaredValue.Text == "")
                {
                    await DisplayAlert("Failure", "Please enter information for all accessory fields", "OK");
                }
                else
                {
                    conn.Insert(newAccessory);
                    await DisplayAlert("Success", "Accessory successfully Created", "OK");
                    await Navigation.PopModalAsync();
                }

            }
        }

        async private void DeclaredValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lets the Entry be empty
            if (string.IsNullOrEmpty(e.NewTextValue)) return;

            if (!double.TryParse(e.NewTextValue, out double value))
            {
                await DisplayAlert("Data Entry Error", "Please enter a numeric value.", "OK");
                ((Entry)sender).Text = e.OldTextValue;
            }
        }
    }
}
