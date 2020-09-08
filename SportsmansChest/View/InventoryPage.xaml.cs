using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using SportsmansChest.Model;
using SportsmansChest.View;
using SQLite;


namespace SportsmansChest.View
{
    public partial class InventoryPage : ContentPage
    {

        public InventoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //data to test with
            App.InventoryTestingData(App.UserLoggedIn);

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();
                var items = conn.Table<InventoryItem>().ToList();

                var userItems = (from InventoryItem in items
                                 where InventoryItem.Id == App.UserLoggedIn
                                 select InventoryItem).ToList();

                itemListView.ItemsSource = userItems;
            }
        }

        async private void ItemListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedInventoryItem = itemListView.SelectedItem as InventoryItem;

            if (selectedInventoryItem != null)
                await Navigation.PushAsync(new InventoryItemPage(selectedInventoryItem));
        }

        async void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddInventoryItemPage()));
        } 

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();
                var items = conn.Table<InventoryItem>().ToList();

                if (string.IsNullOrEmpty(e.NewTextValue))
                {
                    itemListView.ItemsSource = items;
                }
                else
                {
                    var foundItems = (from InventoryItem in items
                                      where InventoryItem.Manufacturer.ToUpper().StartsWith(e.NewTextValue.ToUpper())
                                      select InventoryItem).ToList();
                    itemListView.ItemsSource = foundItems;
                }
            }
        }
    }
}
