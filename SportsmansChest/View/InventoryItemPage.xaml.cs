using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using SportsmansChest.Model;
using SportsmansChest.View;

namespace SportsmansChest.View
{
    public partial class InventoryItemPage : ContentPage
    {
        private InventoryItem selectedInventoryItem;

        public InventoryItemPage(InventoryItem selectedInventoryItem)
        {
            InitializeComponent();

            this.selectedInventoryItem = selectedInventoryItem;

            Manufacturer.Text = selectedInventoryItem.Manufacturer;
            Model.Text = selectedInventoryItem.Model;
            Grade.Text = selectedInventoryItem.Grade;
            SerialNumnber.Text = selectedInventoryItem.SerialNumber;
            DeclairedValue.Text = selectedInventoryItem.DeclairedValue;
            MaintenanceDate.Text = selectedInventoryItem.MaintenanceDate.ToString(App.dateFormat);
            Notification.Text = selectedInventoryItem.Notification;
            Notes.Text = selectedInventoryItem.Notes;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<InventoryItem>();

            //    var itemTable = conn.Table<InventoryItem>().ToList();

            //    var inventoryItem = (from InventoryItem in itemTable
            //                         where InventoryItem.Id == selectedInventoryItem.Id
            //                         select InventoryItem).ToList();

            //    ItemDetailsListView.ItemsSource = inventoryItem;
            //}
        }

        void Accessories_Clicked(System.Object sender, System.EventArgs e)
        {
           Navigation.PushAsync(new AccessoriesPage(selectedInventoryItem));
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

                var confirmationAccept = await DisplayAlert("Delete", "Delete this item?", "Yes", "No");
                if (confirmationAccept)
                {
                    conn.Delete(selectedInventoryItem);
                    await Navigation.PopAsync();
                }
                else
                {

                }
            }
        }
    }
}
