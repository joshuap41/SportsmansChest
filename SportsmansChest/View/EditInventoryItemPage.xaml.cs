using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SQLite;
using SportsmansChest.Model;

namespace SportsmansChest.View
{
    public partial class EditInventoryItemPage : ContentPage
    {
        private InventoryItem selectedInventoryItem;

        public EditInventoryItemPage(InventoryItem selectedInventoryItem)
        {
            InitializeComponent();

            this.selectedInventoryItem = selectedInventoryItem;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            description.Text = selectedInventoryItem.Description;
            manufacturer.Text = selectedInventoryItem.Manufacturer;
            model.Text = selectedInventoryItem.Model;
            grade.SelectedItem = selectedInventoryItem.Grade;
            serialNumber.Text = selectedInventoryItem.SerialNumber;
            declaredValue.Text = selectedInventoryItem.DeclaredValue;
            maintenanceDate.Date = selectedInventoryItem.MaintenanceDate;
            notification.SelectedItem = selectedInventoryItem.Notification;
            notes.Text = selectedInventoryItem.Notes;
        }

        async void CancelToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedInventoryItem.Manufacturer = manufacturer.Text;
            selectedInventoryItem.Description = description.Text;
            selectedInventoryItem.Model = model.Text;
            selectedInventoryItem.Grade = Convert.ToString(grade.SelectedItem);
            selectedInventoryItem.SerialNumber = serialNumber.Text;
            selectedInventoryItem.DeclaredValue = declaredValue.Text;
            selectedInventoryItem.MaintenanceDate = maintenanceDate.Date;
            selectedInventoryItem.Notification = Convert.ToString(notification.SelectedItem);
            selectedInventoryItem.Notes = notes.Text;

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                if (string.IsNullOrWhiteSpace(description.Text) || string.IsNullOrWhiteSpace(manufacturer.Text) || string.IsNullOrWhiteSpace(model.Text) ||
                    string.IsNullOrWhiteSpace(Convert.ToString(grade.SelectedItem)) || string.IsNullOrWhiteSpace(serialNumber.Text) ||
                    string.IsNullOrWhiteSpace(declaredValue.Text) || string.IsNullOrWhiteSpace(Convert.ToString(notification.SelectedItem)))
                {
                    await DisplayAlert("Failure", "Please enter information for all item fields", "OK");
                }
                else
                {
                    conn.Update(selectedInventoryItem);
                    await DisplayAlert("Success", "Item successfully updated", "OK");
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
