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

            //App.UserForTesting();
            base.OnAppearing();
        }

        async void LoginButton_Clicked(System.Object sender, System.EventArgs e)
        {
            // Adds a specific user for testing.
            

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();
                var users = conn.Table<User>().ToList();

                var userId = 0;
                bool userExists = false;
                var testUserNameCheck = string.Empty;

                foreach (User user in users)
                {
                    userId++;

                    if (user.UserName == username.Text && user.UserPassword == password.Text)
                    {
                        userExists = true;
                        App.UserLoggedIn = user.Id;
                        testUserNameCheck = user.UserName;

                    }
                    else
                    {
                        userExists = false;
                    }
                }

                //Needs work here



                if (!string.IsNullOrWhiteSpace(username.Text) || !string.IsNullOrWhiteSpace(password.Text))
                {
                    if (userExists)
                    {
                        if (testUserNameCheck == "test")
                        {
                            await Navigation.PushAsync(new MainPage());
                        }
                        else
                        {
                            App.UserForTesting()
                            await Navigation.PushAsync(new MainPage());
                        }
                        
                    }
                    else
                    {
                        await DisplayAlert("User Error", "The use does not exist", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Input", "Please enter the username and password", "OK");

                }
                
            }
        }

        void RegisterNewUser_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new RegisterNewUserPage()));
        }

    }
}
