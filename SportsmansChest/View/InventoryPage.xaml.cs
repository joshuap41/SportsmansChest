using System;
using System.Collections.Generic;


using Xamarin.Forms;
using SportsmansChest.View;
using SportsmansChest.Model;
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

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();
                var items = conn.Table<InventoryItem>().ToList();
                itemListView.ItemsSource = items;
            }
        }

        private void ItemListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        void AddButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new AddInventoryItemPage());
        }

        void HomeToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
        }
    }
}
