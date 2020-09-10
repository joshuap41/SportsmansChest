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
                                  where InventoryItem.CurrentUser == App.UserLoggedIn
                                  orderby InventoryItem.Id
                                  select InventoryItem.Manufacturer).Distinct().ToList();

                var manufacturerCategoriesCount = categories.Count();

                return manufacturerCategoriesCount;
            }
        }

        public static int InventoryItemCount()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                var inventoryList = conn.Table<InventoryItem>().ToList();

                var userItems = (from InventoryItem in inventoryList
                                 where InventoryItem.CurrentUser == App.UserLoggedIn
                                 select InventoryItem).ToList();

                var inventoryItemCount = userItems.Count();

                return inventoryItemCount;
            }
        }

        public static int TotalLocationCount()
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();

                var locationList = conn.Table<LocationDb>().ToList();

                var userLocations = (from Location in locationList
                                     where Location.CurrentUser == App.UserLoggedIn
                                     select Location).ToList();

                var createdLocationCount = userLocations.Count();

                return createdLocationCount;
            }
        }


        public static void LocationTestingData()
        {
            var locCount = TotalLocationCount();

            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();

                var location = conn.Table<LocationDb>().ToList();

                if (locCount < 1)
                {
                    LocationDb newLocaiton = new LocationDb()
                    {
                        CurrentUser = UserLoggedIn,
                        LocationName = "Favorite Hunting Spot",
                        Longitude = -84.317241,
                        Latitude = 34.736907,
                        EventType = "Hunting",
                        CreatedDate = DateTime.Today,
                        ReturnDate = DateTime.Today,
                        Notification = "Enabled",
                        Notes = "This testing data is for the assessor and will appear at the login page with any new user created"
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

                User newUser = new User()
                {
                    Id = 1,
                    UserName = "test",
                    UserPassword = "test"
                };
                conn.Insert(newUser);
            }
        }


        public static void InventoryTestingData()
        {
            var invCount = InventoryItemCount();

            using (SQLiteConnection conn = new SQLiteConnection(DatabaseLocation))
            {
                conn.CreateTable<User>();
                var testUser = conn.Table<User>().ToList();

                conn.CreateTable<InventoryItem>();
                var item = conn.Table<InventoryItem>().ToList();

                conn.CreateTable<Accessory>();

                if (invCount < 1)
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
                        Notes = "This testing data is for the assessor and will appear at the login page with any new user created"

                    };
                    conn.Insert(newItem);

                    Accessory newAccessory = new Accessory()
                    {
                        Manufacturer = "AR",
                        Model = "95c",
                        SerialNumber = "89352413",
                        DeclairedValue = "200.00",
                        CreatedDate = DateTime.Today,
                        Notes = "This testing data is for the assessor and will appear at the login page with any new user created",
                        InvItem = newItem.Id
                    };
                    conn.Insert(newAccessory);

                }
            }
        }
    }
}
