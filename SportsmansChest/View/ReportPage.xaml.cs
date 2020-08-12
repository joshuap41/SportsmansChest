using System;
using System.Collections.Generic;


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
            //different item Manufacturers
            var manufacturerCategoriesCount = App.ManufacturerCategoriesCount();
            DifferentManufacturers.Text = manufacturerCategoriesCount.ToString();
        }

        async void ExitToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
