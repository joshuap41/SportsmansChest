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
            this.selectedAccessory = selectedAccessory;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Manufacturer.Text = selectedAccessory.Manufacturer;
            Model.Text = selectedAccessory.Model;
            SerialNumnber.Text = selectedAccessory.SerialNumber;
            DeclairedValue.Text = selectedAccessory.DeclairedValue;
            MaintenanceDate.Text = selectedAccessory.MaintenanceDate.ToString(App.dateFormat);
            Notification.Text = selectedAccessory.Notification;
            Notes.Text = selectedAccessory.Notes;

            base.OnAppearing();

            //unable to bind to the tableview need to update fast somehow
            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Accessory>();

            //    var accessoryTable = conn.Table<Accessory>().ToList();

            //    var accessory = (from Accessory in accessoryTable
            //                     where Accessory.Id == selectedAccessory.Id
            //                     select Accessory).ToList();

            //    AccessoryListView.ItemsSource = accessory;
            //}
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
