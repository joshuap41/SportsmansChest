using System;
using SQLite;

namespace SportsmansChest.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string UserPassword { get; set; }

        // Forgot Password?
    }
}
