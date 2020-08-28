using System;
using System.Collections.Generic;
using System.Text;

namespace SportsmansChest.Model
{
    public class StatisticsReport
    {
        //private string r;

        public string GetUserInfo()
        {
            SportmsnsChestUser user1 = new SportmsnsChestUser("Hunting", "Joshua");
            return user1.ShowDetails();
        }

        public class User
        {
            public string Name { get; set; }

            public User(string name)
            {
                this.Name = name;
            }

            public virtual string ShowDetails()
            {
                StringBuilder details = new StringBuilder("Name: " + Name);
                return details.ToString();
            }
        }



        public class SportmsnsChestUser : User
        {
            public string UserInterest { get; set; }

            public SportmsnsChestUser(string userInterest, string name) : base(name)
            {
                this.UserInterest = userInterest;
            }

            public override string ShowDetails()
            {
                //SportmsnsChestUser user1 = new SportmsnsChestUser("Hunting", "Joshua", "Male");
                StringBuilder details = new StringBuilder("User Interest: " + UserInterest + ";  Name: " + Name);

                return details.ToString();
            }
        }
    }



    

}
