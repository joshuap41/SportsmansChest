using System;
using SQLite;

namespace SportsmansChest.Model
{
    public class Accessory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //need a "nickName for the individual items to further decifer each one
        public int InvItem { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string DeclairedValue { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string Notification { get; set; }
        public string Notes { get; set; }
    }
}
