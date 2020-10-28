using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SportsmansChest.Model;
using SQLite;
using System.Text;
using Xamarin.Essentials;

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
            Description.Text = selectedAccessory.Description;
            Manufacturer.Text = selectedAccessory.Manufacturer;
            Model.Text = selectedAccessory.Model;
            SerialNumnber.Text = selectedAccessory.SerialNumber;
            DeclaredValue.Text = selectedAccessory.DeclaredValue.ToString();
            CreatedDate.Text = selectedAccessory.CreatedDate.ToString(App.dateFormat);
            Notes.Text = selectedAccessory.Notes;

            base.OnAppearing();
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
            }
        }

        async void EditAccessory_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditAccessoryPage(selectedAccessory)));
        }

        async void Share_Clicked(System.Object sender, System.EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Saved Associated Accessory Information from the Sportsman's Chest Mobile Application.");
            sb.AppendLine($"");
            sb.AppendLine($"Description:  {Description.Text}");
            sb.AppendLine($"Manufacturer:  {Manufacturer.Text}");
            sb.AppendLine($"Model:  {Model.Text}");
            sb.AppendLine($"Serial Number:  {SerialNumnber.Text}");
            sb.AppendLine($"Declared Value : $ {DeclaredValue.Text}");
            sb.AppendLine($"Created Date:  {CreatedDate.Text}");
            sb.AppendLine($"Notes:  {Notes.Text}");

            await Share.RequestAsync(new ShareTextRequest
            {
                Subject = "Associated Accessory Information",
                Text = sb.ToString(),
                Title = "Share your notes on the course"

            });
        }
    }
}
