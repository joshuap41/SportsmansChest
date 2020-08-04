using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;
using SportsmansChest.Model;


namespace SportsmansChest.View
{
    public partial class AccessoryPage : ContentPage
    {
        InventoryItem selectedInventoryItem;

        public AccessoryPage(InventoryItem selectedInventoryItem)
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

                var createdAccessoryList = (from accessory in accessoryTable
                                        where accessory.Id == selectedInventoryItem.Id
                                        select accessory).ToList();

                accessoryListView.ItemsSource = createdAccessoryList;
            }
        }
    }
}
