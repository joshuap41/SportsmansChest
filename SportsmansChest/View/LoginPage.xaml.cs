using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using SportsmansChest.Model;

using Xamarin.Forms;

namespace SportsmansChest.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            
            username.Text = string.Empty;
            password.Text = string.Empty;
            base.OnAppearing();
        }

        async void LoginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                var users = conn.Table<User>().ToList();

                var userId = 0;

                if (!string.IsNullOrWhiteSpace(username.Text) || !string.IsNullOrWhiteSpace(password.Text))
                {
                    bool userExists = false;

                    foreach (User user in users)
                    {
                        userId++;

                        if (user.UserName == username.Text && user.UserPassword == password.Text)
                        {
                            userExists = true;
                            App.UserLoggedIn = user.Id;
                            App.LocationTestingData();
                            App.InventoryTestingData();

                            // Change this to happen without the App being open.
                            App.DisplayNotifications();
                            await Navigation.PushAsync(new MainPage());
                            return;
                        }
                        else
                        {
                            userExists = false;
                        }
                    }
                    await DisplayAlert("Invalid Input", "This user does not exist. Please register a new user.", "OK");
                }
                else
                {
                    await DisplayAlert("Invalid Input", "Please enter the username and password.", "OK");
                }
                
            }
        }

        void RegisterNewUser_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new RegisterNewUserPage()));
        }

    }
}
