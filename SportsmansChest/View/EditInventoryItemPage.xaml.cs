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

            manufacturer.Text = selectedInventoryItem.Manufacturer;
            model.Text = selectedInventoryItem.Model;
            grade.SelectedItem = selectedInventoryItem.Grade;
            serialNumber.Text = selectedInventoryItem.SerialNumber;
            declairedValue.Text = selectedInventoryItem.DeclairedValue;
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
            selectedInventoryItem.Model = model.Text;
            selectedInventoryItem.Grade = Convert.ToString(grade.SelectedItem);
            selectedInventoryItem.SerialNumber = serialNumber.Text;
            selectedInventoryItem.DeclairedValue = declairedValue.Text;
            selectedInventoryItem.MaintenanceDate = maintenanceDate.Date;
            selectedInventoryItem.Notification = Convert.ToString(notification.SelectedItem);
            selectedInventoryItem.Notes = notes.Text;

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();
                conn.Update(selectedInventoryItem);
                //validate user input
            }
            await Navigation.PopModalAsync();
            //await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
