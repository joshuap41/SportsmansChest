using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;



using Xamarin.Forms;

namespace SportsmansChest.View
{
    public partial class ReportPage : ContentPage
    {
        public ReportPage()
        {
            InitializeComponent();

            //total Inventory Items
            var totalInventoryItemCount = App.InventoryItemCount();
            TotalInvCount.Text = totalInventoryItemCount.ToString();
            //Unique item Manufacturers
            var manufacturerCategoriesCount = App.ManufacturerCategoriesCount();
            UniqueItemManufacturers.Text = manufacturerCategoriesCount.ToString();
            //Displays the Report run date
            var reportRunDate = DateTime.Now;
            ReportRunDate.Text = reportRunDate.ToString(App.dateFormat);
        }

        async void ExitToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
