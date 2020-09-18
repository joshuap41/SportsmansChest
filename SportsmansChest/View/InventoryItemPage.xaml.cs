using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using SportsmansChest.Model;
using Xamarin.Essentials;
using System.Text;

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
            DeclaredValue.Text = selectedInventoryItem.DeclaredValue;
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

        async void Share_Clicked(System.Object sender, System.EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Saved Inventory Item Information from the Sportsman's Chest Mobile Application.");
            sb.AppendLine($"");
            sb.AppendLine($"Description:  {Description.Text}");
            sb.AppendLine($"Manufacturer:  {Manufacturer.Text}");
            sb.AppendLine($"Model:  {Model.Text}");
            sb.AppendLine($"Grade:  {Grade.Text}");
            sb.AppendLine($"Serial Number:  {SerialNumnber.Text}");
            sb.AppendLine($"Declared Value : $ {DeclaredValue.Text}");
            sb.AppendLine($"Created Date:  {CreatedDate.Text}");
            sb.AppendLine($"Maintenance Date:  {MaintenanceDate.Text}");
            sb.AppendLine($"Notes:  {Notes.Text}");

            await Share.RequestAsync(new ShareTextRequest
            {
                Subject = "Inventory Item Information",
                Text = sb.ToString(),
                Title = "Share your notes on the course"

            });
        }
    }
}
