using System;
using System.Collections.Generic;
using SQLite;
using SportsmansChest.Model;
using System.Linq;

using Xamarin.Forms;

namespace SportsmansChest.View
{
    public partial class RegisterNewUserPage : ContentPage
    {
        public RegisterNewUserPage()
        {
            InitializeComponent();
        }

        async void RegisterUserButton_Clicked(System.Object sender, System.EventArgs e)
        {
            User user = new User()
            {
                UserName = username.Text,
                UserPassword = password.Text
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<User>();

                if (!string.IsNullOrWhiteSpace(username.Text) || !string.IsNullOrWhiteSpace(password.Text) || !string.IsNullOrWhiteSpace(confirmPassword.Text))
                {
                    if (password.Text == confirmPassword.Text)
                    {
                        conn.Insert(user);
                        await DisplayAlert("Success","The new user was successfully created","OK");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Invalid Input", "The password must match the confirmation password", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Input", "Please enter a username, password and a confirmation password", "OK");
                }
            }
        }

        void CancelButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
