using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsmansChest.Model
{
    class InventoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Grade { get; set; }
        public int SerialNumber { get; set; }
        public float DeclairedValue { get; set; }
        public int Notification { get; set; }
        public string Notes { get; set; }

        //pictures...
    }
}
