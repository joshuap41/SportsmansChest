using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SportsmansChest.View;
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

            using (SQLiteConnection conn = new SQLiteConnection)
            {
                conn.CreateTable<InventoryItem>();
                var items = conn.Table<InventoryItem>().ToList();

                conn.CreateTable<Accessory>();
                var accessories = conn.Table<Accessory>().ToList();

                try
                {
                    if (appOpening)
                    {
                        var itemId = 0;

                        var accessoryId = 0;

                        appOpening = false;

                        foreach (var InventoryItem in items)
                        {
                            if (InventoryItem.Notification == "Enabled")
                            {
                                if (InventoryItem.MaintenanceDate == DateTime.Today)
                                {
                                    CrossLocal
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Failure", "Notifications failed to be displayed", "Ok");
                }
            }
        }

        void InventoryList_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new InventoryPage());
        }

        void PastEvents_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
