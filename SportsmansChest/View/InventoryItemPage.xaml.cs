using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SQLite;
using SportsmansChest.Model;
using SportsmansChest.View;

namespace SportsmansChest.View
{
    public partial class InventoryItemPage : ContentPage
    {
        public InventoryItemPage(InventoryItem selectedInventoryItem)
        {
            InitializeComponent();
        }
    }
}
