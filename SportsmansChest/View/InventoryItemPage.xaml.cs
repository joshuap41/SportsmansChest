using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using SportsmansChest.Model;

namespace SportsmansChest.View
{
    public partial class InventoryItemPage : ContentPage
    {
        private InventoryItem selectedInventoryItem;

        public InventoryItemPage(InventoryItem selectedInventoryItem)
        {
            this.selectedInventoryItem = selectedInventoryItem;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Description.Text = selectedInventoryItem.Description;
            Manufacturer.Text = selectedInventoryItem.Manufacturer;
            Model.Text = selectedInventoryItem.Model;
            Grade.Text = selectedInventoryItem.Grade;
            SerialNumnber.Text = selectedInventoryItem.SerialNumber;
            DeclairedValue.Text = selectedInventoryItem.DeclairedValue;
            CreatedDate.Text = selectedInventoryItem.CreatedDate.ToString(App.dateFormat);
            MaintenanceDate.Text = selectedInventoryItem.MaintenanceDate.ToString(App.dateFormat);
            Notification.Text = selectedInventoryItem.Notification;
            Notes.Text = selectedInventoryItem.Notes;

            base.OnAppearing();
        }

        async void Accessories_Clicked(System.Object sender, System.EventArgs e)
        {
           await Navigation.PushAsync(new AccessoriesPage(selectedInventoryItem));
        }

        async void EditItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditInventoryItemPage(selectedInventoryItem)));
        }

        async void DeleteItem_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();
                conn.CreateTable<Accessory>();
                var accessory = conn.Table<Accessory>().ToList();

                var confirmationAccept = await DisplayAlert("Delete", "Delete this item?", "Yes", "No");
                if (confirmationAccept)
                {
                    conn.Delete(selectedInventoryItem);

                    // Delete all associated accessories for the specific item.
                    conn.Execute($"DELETE FROM Accessory WHERE InvItem = '{selectedInventoryItem.Id}'");

                    await Navigation.PopAsync();
                }
            }
        }
    }
}
