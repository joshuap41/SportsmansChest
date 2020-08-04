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
                Manufacturer = manufacturer.Text,
                Model = model.Text,
                SerialNumber = serialNumber.Text,
                DeclairedValue = declairedValue.Text,
                MaintenanceDate = maintenanceDate.Date,
                //Notification Need to wrap in a "Switch Cell" to use like the other project...
                Notification = Convert.ToString(notification.SelectedItem),
                Notes = notes.Text
            };

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();
                conn.Insert(newAccessory);
                await Navigation.PopModalAsync();
                //Validate user input here

            }
        }
    }
}
