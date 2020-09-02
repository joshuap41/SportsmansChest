using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using SportsmansChest.Model;



namespace SportsmansChest.View
{
    public partial class AccessoriesPage : ContentPage
    {
        InventoryItem selectedInventoryItem;

        public AccessoriesPage(InventoryItem selectedInventoryItem)
        {
            InitializeComponent();

            this.selectedInventoryItem = selectedInventoryItem;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();
                var accessoryTable = conn.Table<Accessory>().ToList();
                //sifting for correct accessory for the item
                var createdAccessoryList = (from accessory in accessoryTable
                                            where accessory.InvItem == selectedInventoryItem.Id
                                            select accessory).ToList();

                AccessoriesListView.ItemsSource = createdAccessoryList;
            }
        }

        async void AddToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddAccessoryPage(selectedInventoryItem)));
        }

        async void AccessoriesListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var selectedAccessory = AccessoriesListView.SelectedItem as Accessory;

            if (selectedAccessory != null)
                await Navigation.PushAsync(new AccessoryPage(selectedAccessory));
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();
                var associatedAccessories = conn.Table<Accessory>().ToList();

                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    var defaultAccessories = (from Accessory in associatedAccessories
                                              where Accessory.InvItem == selectedInventoryItem.Id
                                              select Accessory).ToList();
                    AccessoriesListView.ItemsSource = defaultAccessories;
                }
                else
                {
                    var foundAccessories = (from Accessory in associatedAccessories
                                      where (Accessory.Manufacturer.ToUpper().StartsWith(e.NewTextValue.ToUpper())) &&
                                      (Accessory.InvItem == selectedInventoryItem.Id)
                                      select Accessory).ToList();
                    AccessoriesListView.ItemsSource = foundAccessories;
                }
            }
        }
    }
}
