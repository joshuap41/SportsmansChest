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
                MaintenanceDate = maintenanceDate.Date,
                //been here done this but WTF
                //Notification = notification.On == true ? 1 : 0,
                Notes = notes.Text
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                conn.Insert(newItem);
                await Navigation.PopModalAsync();
                //validate user input


            }
        }
    }
}
