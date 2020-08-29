using System;
using System.Collections.Generic;
using System.Text;

namespace SportsmansChest.Model
{
    public class StatisticsReport
    {
        public static string GetUserInfo()
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
            //virtual allows the derived class method to be called.
            public virtual string ShowDetails()
            {
                StringBuilder details = new StringBuilder("Name: " + Name);
                return details.ToString();
            }
        }


        // Derived class from User-- Inheritance.
        public class SportmsnsChestUser : User
        {
            public string UserInterest { get; set; }

            public SportmsnsChestUser(string userInterest, string name) : base(name)
            {
                this.UserInterest = userInterest;
            }
            // used the override access modifier to allow Polymorphism to be used.
            public override string ShowDetails()
            {
                //SportmsnsChestUser user1 = new SportmsnsChestUser("Hunting", "Joshua", "Male");
                StringBuilder details = new StringBuilder("Interest: " + UserInterest + ";  Name: " + Name);

                return details.ToString();
            }
        }
    }



    

}
