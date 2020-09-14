using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using SportsmansChest.Model;
using SportsmansChest.View;
using System.Linq;
using SQLite;
using Plugin.LocalNotifications;
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
                        Notes = "This testing data is for the assessor and is created at the login page with any new user created that doesn't contain any data"
                    };
                    conn.Insert(newLocaiton);

                    LocationDb newLocaiton2 = new LocationDb()
                    {
                        CurrentUser = UserLoggedIn,
                        LocationName = "Favorite Hunting Spot 2",
                        Longitude = -84.317241,
                        Latitude = 34.736907,
                        EventType = "Hunting",
                        CreatedDate = DateTime.Today,
                        ReturnDate = DateTime.Today,
                        Notification = "Enabled",
                        Notes = "This testing data is for the assessor and is created at the login page with any new user created that doesn't contain any data"
                    };
                    conn.Insert(newLocaiton2);
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
                        Description = "First Gun",
                        Manufacturer = "Browning",
                        Model = "CB16",
                        Grade = "Standard",
                        SerialNumber = "589764",
                        DeclairedValue = "500.00",
                        CreatedDate = DateTime.Today,
                        MaintenanceDate = DateTime.Today,
                        Notification = "Enabled",
                        Notes = "This testing data is for the assessor and is created at the login page with any new user created that doesn't contain any data"

                    };
                    conn.Insert(newItem);

                    InventoryItem newItem2 = new InventoryItem()
                    {
                        CurrentUser = UserLoggedIn,
                        Description = "First Gun 2",
                        Manufacturer = "Browning",
                        Model = "CB16",
                        Grade = "Standard",
                        SerialNumber = "589764",
                        DeclairedValue = "500.00",
                        CreatedDate = DateTime.Today,
                        MaintenanceDate = DateTime.Today,
                        Notification = "Enabled",
                        Notes = "This testing data is for the assessor and is created at the login page with any new user created that doesn't contain any data"

                    };
                    conn.Insert(newItem2);

                    Accessory newAccessory = new Accessory()
                    {
                        Description = "First Scope",
                        Manufacturer = "AR",
                        Model = "95c",
                        SerialNumber = "89352413",
                        DeclairedValue = "200.00",
                        CreatedDate = DateTime.Today,
                        Notes = "This testing data is for the assessor and is created at the login page with any new user created that doesn't contain any data",
                        InvItem = newItem.Id
                    };
                    conn.Insert(newAccessory);

                    Accessory newAccessory2 = new Accessory()
                    {
                        Description = "First Scope 2",
                        Manufacturer = "AR",
                        Model = "95c",
                        SerialNumber = "89352413",
                        DeclairedValue = "200.00",
                        CreatedDate = DateTime.Today,
                        Notes = "This testing data is for the assessor and is created at the login page with any new user created that doesn't contain any data",
                        InvItem = newItem.Id
                    };
                    conn.Insert(newAccessory2);
                }
            }
        }

        public static void DisplayNotifications()
        {
            bool invAppOpening = true;
            bool locAppOpening = true;

            // Notifications
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();
                var itemsList = conn.Table<InventoryItem>().ToList();

                conn.CreateTable<LocationDb>();
                var locations = conn.Table<LocationDb>().ToList();

                try
                {
                    if (invAppOpening)
                    {
                        var itemId = 0;
                        invAppOpening = false;

                        foreach (InventoryItem inventoryItem in itemsList)
                        {
                            itemId++;
                            if (inventoryItem.Notification == "Enabled")
                            {
                                if (inventoryItem.MaintenanceDate == DateTime.Today)
                                {
                                    if (inventoryItem.CurrentUser == App.UserLoggedIn)
                                    {
                                        // need a "nickName for the individual items to further decifer each one
                                        CrossLocalNotifications.Current.Show("Notification Received", $"Inventory Item: {inventoryItem.Manufacturer} needs maintenance today.", itemId);
                                    }
                                }
                            }
                        }
                    }

                    if (locAppOpening)
                    {
                        var locationId = 0;
                        locAppOpening = false;

                        foreach (LocationDb location in locations)
                        {
                            locationId++;
                            if (location.Notification == "Enabled")
                            {
                                if (location.ReturnDate == DateTime.Today)
                                {
                                    if (location.CurrentUser == App.UserLoggedIn)
                                    {
                                        CrossLocalNotifications.Current.Show("Notification Received", $"Location: {location.LocationName} needs to be visited today", locationId);
                                    }
                                }
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

        // Need to test this somehow
        private static void DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        public static void DeleteAlInventoryItems()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<InventoryItem>();

                var rowsBefore = conn.Table<InventoryItem>().Count();

                conn.Execute("DELETE FROM InventoryItem");

                var rowsAfter = conn.Table<InventoryItem>().Count();

            }
        }

        public static void DeleteAllAccessories()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();

                var rowsBefore = conn.Table<Accessory>().Count();

                conn.Execute("DELETE FROM Accessory");

                var rowsAfter = conn.Table<Accessory>().Count();

            }
        }

        public static void DeleteAllLocations()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();

                var rowsBefore = conn.Table<LocationDb>().Count();

                conn.Execute("DELETE FROM LocationDb");

                var rowsAfter = conn.Table<LocationDb>().Count();

            }
        }
    }
}
