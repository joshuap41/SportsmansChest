﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SportsmansChest.View
{
    public partial class ReportPage : ContentPage
    {
        public ReportPage()
        {
            InitializeComponent();

            // total Inventory Items
            var totalInventoryItemCount = App.InventoryItemCount();
            TotalInvCount.Text = totalInventoryItemCount.ToString();

            // unique item Manufacturers
            var manufacturerCategoriesCount = App.ManufacturerCategoriesCount();
            UniqueItemManufacturers.Text = manufacturerCategoriesCount.ToString();

            // total created locations
            var totalLocationCount = App.TotalLocationCount();
            TotalLocationCount.Text = totalLocationCount.ToString();
            
            //Displays the Report run date
            var reportCreationDate = DateTime.Now;
            ReportCreationDate.Text = reportCreationDate.ToString(App.dateFormat);
        }

        async void ExitToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
