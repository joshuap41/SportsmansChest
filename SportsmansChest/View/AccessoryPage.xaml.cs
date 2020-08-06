using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SportsmansChest.Model;
using SQLite;
using System.Linq;

namespace SportsmansChest.View
{
    public partial class AccessoryPage : ContentPage
    {
        private Accessory selectedAccessory;

        public AccessoryPage(Accessory selectedAccessory)
        {
            InitializeComponent();

            this.selectedAccessory = selectedAccessory;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();

                var accessoryTable = conn.Table<Accessory>().ToList();

                var accessory = (from Accessory in accessoryTable
                                 where Accessory.Id == selectedAccessory.Id
                                 select Accessory).ToList();

                AccessoryListView.ItemsSource = accessory;
            }
        }

        async void DeleteAccessory_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();

                var confirmationAccept = await DisplayAlert("Delete", "Delete this accessory?", "Yes", "No");
                if (confirmationAccept)
                {
                    conn.Delete(selectedAccessory);
                    await Navigation.PopAsync();
                }
                else
                {

                }
            }
        }

        async void EditAccessory_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditAccessoryPage(selectedAccessory)));
        }
    }
}
