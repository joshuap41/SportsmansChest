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
                CurrentUser = App.UserLoggedIn,
                Description = description.Text,
                Manufacturer = manufacturer.Text,
                Model = model.Text,
                Grade = Convert.ToString(grade.SelectedItem),
                SerialNumber = serialNumber.Text,
                DeclaredValue = Convert.ToDouble(declaredValue.Text),
                MaintenanceDate = maintenanceDate.Date,
                CreatedDate = DateTime.Now,
                Notification = Convert.ToString(notification.SelectedItem),
                Notes = notes.Text
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                if (string.IsNullOrWhiteSpace(description.Text) || string.IsNullOrWhiteSpace(manufacturer.Text) || string.IsNullOrWhiteSpace(model.Text) ||
                    string.IsNullOrWhiteSpace(Convert.ToString(grade.SelectedItem)) || string.IsNullOrWhiteSpace(serialNumber.Text) ||
                    declaredValue.Text == "" || string.IsNullOrWhiteSpace(Convert.ToString(notification.SelectedItem)))
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
