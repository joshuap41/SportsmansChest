using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using SportsmansChest.Model;
using SportsmansChest.View;
using System.Linq;
using SQLite;
using System.Diagnostics;

namespace SportsmansChest
{
    public partial class App : Application
    {
        public static string dateFormat = " MM/dd/yyyy";

        public static string DatabaseLocation = string.Empty;

        public static int UserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new LoginPage());
        }

        //used for DB
        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());

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

        public static int TotalLocationCount()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();

                var locationList = conn.Table<LocationDb>().ToList();

                var createdLocationCount = locationList.Count();

                return createdLocationCount;
            }
        }


        public static void LocationTestingData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();

                var location = conn.Table<LocationDb>().ToList();

                if (!location.Any())
                {
                    LocationDb newLocaiton = new LocationDb()
                    {
                        LocationName = "Favorite Hunting Spot",
                        Longitude = -84.317241,
                        Latitude = 34.736907,
                        EventType = "Hunting",
                        CreatedDate = DateTime.Today,
                        ReturnDate = DateTime.Today,
                        Notification = "Enabled",
                        Notes = "I saw a lot of bucks by the stream here!"
                    };
                    conn.Insert(newLocaiton);
                }
            }
        }

        public static void UserForTesting()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<User>();
                var testUser = conn.Table<User>().ToList();

                if (!testUser.Any())
                {
                    User newUser = new User()
                    {
                        UserName = "test",
                        UserPassword = "test"
                    };
                    conn.Insert(newUser);
                }
            }
        }


        public static void InventoryTestingData(int UserLoggedIn)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<User>();
                var testUser = conn.Table<User>().ToList();

                conn.CreateTable<InventoryItem>();
                var item = conn.Table<InventoryItem>().ToList();

                conn.CreateTable<Accessory>();

                if (!item.Any())
                {
                    InventoryItem newItem = new InventoryItem()
                    {
                        CurrentUser = UserLoggedIn,
                        Manufacturer = "Browning",
                        Model = "CB16",
                        Grade = "Standard",
                        SerialNumber = "589764",
                        DeclairedValue = "500.00",
                        CreatedDate = DateTime.Today,
                        MaintenanceDate = DateTime.Today,
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
                        CreatedDate = DateTime.Today,
                        MaintenanceDate = DateTime.Today,
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
