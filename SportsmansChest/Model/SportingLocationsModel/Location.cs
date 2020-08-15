using System;
using SQLite;

namespace SportsmansChest.Model.SportingLocationsModel
{
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string LocationName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string EventType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Notification { get; set; }
        public string Notes { get; set; }
    }
}