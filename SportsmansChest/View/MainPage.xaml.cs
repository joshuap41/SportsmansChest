using System;
using System.ComponentModel;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using SportsmansChest.View;
using SportsmansChest.View.SportingLocationsViews;
using SportsmansChest.Model;
using SQLite;

namespace SportsmansChest
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            bool appOpening = true;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();
                var itemsList = conn.Table<InventoryItem>().ToList();

                conn.CreateTable<Accessory>();
                var accessories = conn.Table<Accessory>().ToList();

                conn.CreateTable<LocationDb>();
                var locations = conn.Table<LocationDb>().ToList();

                //notifications
                try
                {
                    if (appOpening)
                    {
                        var itemId = 0;
                        var accessoryId = 0;
                        var locationId = 0;
                        appOpening = false;

                        foreach (InventoryItem inventoryItem in itemsList)
                        {
                            itemId++;
                            if (inventoryItem.Notification == "Enabled" && inventoryItem.MaintenanceDate == DateTime.Today)
                            {
                                //need a "nickName for the individual items to further decifer each one
                                CrossLocalNotifications.Current.Show("Notification Received", $"Inventory Item: {inventoryItem.Manufacturer} needs maintenance today.", itemId);
                            }
                        }
                        foreach (Accessory accessory in accessories)
                        {
                            accessoryId++;
                            if (accessory.Notification == "Enabled" && accessory.MaintenanceDate == DateTime.Today)
                            {
                                //need a "nickName for the individual items to further decifer each one
                                CrossLocalNotifications.Current.Show("Notification Received", $"Accessory: {accessory.Manufacturer} needs maintenance today", accessoryId);
                            }
                        }
                        foreach (LocationDb location in locations)
                        {
                            locationId++;
                            if (location.Notification == "Enabled")
                            {
                                CrossLocalNotifications.Current.Show("Notification Received", $"Location: {location.LocationName} needs to be visited today", locationId);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    DisplayAlert("Failure", "Notifications failed to be displayed", "Ok");
                }
            }
        }

        async void InventoryList_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage());
        }

        async void SportingLocations_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LocationsPage());
        }

        async void ReportButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ReportPage()));
        }
    }
}
