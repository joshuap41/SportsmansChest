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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                var itemTable = conn.Table<InventoryItem>().ToList();

                var inventoryItem = (from InventoryItem in itemTable
                                     where InventoryItem.Id == selectedInventoryItem.Id
                                     select InventoryItem).ToList();

                itemDetailsListView.ItemsSource = inventoryItem;
            }
        }

        void Accessories_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        //void Accessories_Clicked_1(System.Object sender, System.EventArgs e)
        //{
        //}

        void EditItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void DeleteItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
