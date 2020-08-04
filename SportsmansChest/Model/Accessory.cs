﻿using System;
using SQLite;

namespace SportsmansChest.Model
{
    public class Accessory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int InventoryItem { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string DeclairedValue { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Notification { get; set; }
        public string Notes { get; set; }
    }
}
