using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SportsmansChest.Model;
using SportsmansChest.View;
using System.Linq;
using SQLite;

namespace SportsmansChest
{
    public partial class App : Application
    {
        public static string dateFormat = " MM/dd/yyyy";

        public static string DatabaseLocation = string.Empty;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MainPage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        

        public static int ManufacturerCategoriesCount()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                var inventoryList = conn.Table<InventoryItem>().ToList();

                var categories = (from InventoryItem in inventoryList
                                  orderby InventoryItem.Id
                                  select InventoryItem.Manufacturer).Distinct().ToList();

                var manufacturerCategoriesCount = categories.Count();

                return manufacturerCategoriesCount;
            }
        }

        public static int AccessoryCount()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<Accessory>();

                var accessoryList = conn.Table<Accessory>().ToList();

                var accessoryCount = accessoryList.Count();

                return accessoryCount;
            }
        }

        public static int InventoryItemCount()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                var inventoryList = conn.Table<InventoryItem>().ToList();

                var inventoryItemCount = inventoryList.Count();

                return inventoryItemCount;
            }
        }

        public static void FillerData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                var item =  conn.Table<InventoryItem>().ToList();

                var inventoryItemList = (from InventoryItem in item
                                   where InventoryItem.Manufacturer == "Browning"
                                   select InventoryItem).Distinct().ToList();

                var lists = (from InventoryItem in inventoryItemList
                             select InventoryItem.Manufacturer == "Browning").ToList();

                conn.CreateTable<Accessory>();

                if (!item.Any())
                {
                    InventoryItem newItem = new InventoryItem()
                    {
                        Manufacturer = "Browning",
                        Model = "CB16",
                        Grade = "Standard",
                        SerialNumber = "589764",
                        DeclairedValue = "500.00",
                        MaintenanceDate = new DateTime(2020, 08, 14),
                        Notification = "Enabled",
                        Notes = "This is my favorite bow to hunt with. I will need to get a maintenance kit for it soon"
                    };
                    conn.Insert(newItem);

                    Accessory newAccessory = new Accessory()
                    {
                        Manufacturer = "AR",
                        Model = "95c",
                        SerialNumber = "89352413",
                        DeclairedValue = "200.00",
                        MaintenanceDate = new DateTime(2020, 08, 14),
                        Notification = "Enabled",
                        Notes = "This is my favorite scope to hunt with.",
                        InvItem = newItem.Id
                    };
                    conn.Insert(newAccessory);
                }
            }
        }
    }
}
