using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;
using SportsmansChest.Model;
using SportsmansChest.View;

namespace SportsmansChest.View
{
    public partial class AddInventoryItemPage : ContentPage
    {
        public AddInventoryItemPage()
        {
            InitializeComponent();
        }

        async void CancelButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveButton_Clicked(System.Object sender, System.EventArgs e)
        {
            InventoryItem newItem = new InventoryItem
            {
                Manufacturer = manufacturer.Text,
                Model = model.Text,
                Grade = Convert.ToString(grade.SelectedItem),
                SerialNumber = serialNumber.Text,
                DeclairedValue = declairedValue.Text,
                CreatedDate = DateTime.Now,
                Notification = Convert.ToString(notification.SelectedItem),
                Notes = notes.Text
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                if (string.IsNullOrWhiteSpace(manufacturer.Text) || string.IsNullOrWhiteSpace(model.Text) ||
                    string.IsNullOrWhiteSpace(Convert.ToString(grade.SelectedItem)) || string.IsNullOrWhiteSpace(serialNumber.Text) ||
                    string.IsNullOrWhiteSpace(declairedValue.Text) || string.IsNullOrWhiteSpace(Convert.ToString(notification.SelectedItem)) ||
                    string.IsNullOrWhiteSpace(notes.Text))
                {
                    await DisplayAlert("Failure", "Please enter information for all item fields", "OK");
                }
                else
                {
                    conn.Insert(newItem);
                    await DisplayAlert("Success", "Item successfully Created", "OK");
                    await Navigation.PopModalAsync();
                }
            }
        }
    }
}
