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
                //sifting for correct accessory for the item
                var createdAccessoryList = (from accessory in accessoryTable
                                            where accessory.InvItem == selectedInventoryItem.Id
                                            select accessory).ToList();

                accessoryListView.ItemsSource = createdAccessoryList;
            }
        }

        async void AddToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddAccessoryPage(selectedInventoryItem)));
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            
        }

        void accessoryListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

        }
    }
}
