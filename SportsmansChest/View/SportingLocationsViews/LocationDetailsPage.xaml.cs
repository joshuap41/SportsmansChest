using System;
using SportsmansChest.Model;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Text;

namespace SportsmansChest.View.SportingLocationsViews
{
    public partial class LocationDetailsPage : ContentPage
    {
        private LocationDb selectedLocation;

        public LocationDetailsPage(LocationDb selectedLocation)
        {
            this.selectedLocation = selectedLocation;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LocationName.Text = selectedLocation.LocationName;
            Longitude.Text = selectedLocation.Longitude.ToString();
            Latitude.Text = selectedLocation.Latitude.ToString();
            EventType.Text = selectedLocation.EventType;
            CreatedDate.Text = selectedLocation.CreatedDate.ToString(App.dateFormat);
            ReturnDate.Text = selectedLocation.ReturnDate.ToString(App.dateFormat);
            Notification.Text = selectedLocation.Notification;
            Notes.Text = selectedLocation.Notes;

            base.OnAppearing();
        }

        async void EditLocation_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditLocationPage(selectedLocation)));
        }

        async void DeleteLocation_Clicked(System.Object sender, System.EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<LocationDb>();

                var confirmationAccept = await DisplayAlert("Delete", "Delete this item?", "Yes", "No");
                if (confirmationAccept)
                {
                    conn.Delete(selectedLocation);
                    await Navigation.PopAsync();
                }
            }
        }

        async void Directions_Clicked(System.Object sender, System.EventArgs e)
        {
            if (!double.TryParse(Latitude.Text, out double lat))
                return;

            if (!double.TryParse(Longitude.Text, out double lon))
                return;

            await Map.OpenAsync(lat, lon, new MapLaunchOptions
            {
                Name = LocationName.Text,
                NavigationMode = NavigationMode.None
            });

        }

        async void Share_Clicked(System.Object sender, System.EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Saved Location Information from the Sportsman's Chest Mobile Application.");
            sb.AppendLine($"");
            sb.AppendLine($"Location Name:  {LocationName.Text}");
            sb.AppendLine($"Longitude:  {Longitude.Text}");
            sb.AppendLine($"Latitude:  {Latitude.Text}");
            sb.AppendLine($"Event Type:  {EventType.Text}");
            sb.AppendLine($"Created Date:  {CreatedDate.Text}");
            sb.AppendLine($"Return Date:  {ReturnDate.Text}");
            sb.AppendLine($"Notes:  {Notes.Text}");

            await Share.RequestAsync(new ShareTextRequest
            {
                Subject = "Sporting Location Information",
                Text = sb.ToString(),
                Title = "Share your notes on the course"
            });
        }
    }
}
