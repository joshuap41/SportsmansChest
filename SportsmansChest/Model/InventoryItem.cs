using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsmansChest.Model
{
    public class InventoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }


        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Grade { get; set; }
        public string SerialNumber { get; set; }
        public string DeclairedValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Notification { get; set; }
        public string Notes { get; set; }

    }
}
