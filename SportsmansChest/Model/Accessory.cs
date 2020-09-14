using System;
using SQLite;

namespace SportsmansChest.Model
{
    public class Accessory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int InvItem { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string DeclairedValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Notes { get; set; }

        // Pictures here... byte?
    }
}
