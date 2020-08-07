﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using SportsmansChest.Model;
using SQLite;


namespace SportsmansChest.View
{
    public partial class EditAccessoryPage : ContentPage
    {
        private Accessory selectedAccessory;

        public EditAccessoryPage(Accessory selectedAccessory)
        {
            InitializeComponent();

            this.selectedAccessory = selectedAccessory;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            manufacturer.Text = selectedAccessory.Manufacturer;
            model.Text = selectedAccessory.Model;
            serialNumber.Text = selectedAccessory.SerialNumber;
            declairedValue.Text = selectedAccessory.DeclairedValue;
            maintenanceDate.Date = selectedAccessory.MaintenanceDate;
            notification.SelectedItem = selectedAccessory.MaintenanceDate;
            notes.Text = selectedAccessory.Notes;
        }

        async void CancelToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void SaveToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {
            selectedAccessory.Manufacturer = manufacturer.Text;
            selectedAccessory.Model = model.Text;
            selectedAccessory.SerialNumber = serialNumber.Text;
            selectedAccessory.DeclairedValue = declairedValue.Text;
            selectedAccessory.MaintenanceDate = maintenanceDate.Date;
            selectedAccessory.Notification = Convert.ToString(notification.SelectedItem);
            selectedAccessory.Notes = notes.Text;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Accessory>();
                conn.Update(selectedAccessory);
                //validate user inptu
            }
            await Navigation.PopModalAsync();
        }
    }
}