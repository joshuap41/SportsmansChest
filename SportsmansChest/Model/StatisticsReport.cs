using System;
using System.Collections.Generic;
using System.Text;

namespace SportsmansChest.Model
{
    public class StatisticsReport
    {
        public static string GetUserInfo()
        {
            SportsmansChestDeveloper user1 = new SportsmansChestDeveloper("Hunting", "Joshua");
            return user1.ShowDetails();
        }

        public class Developer
        {
            public string Name { get; set; }

            public Developer(string name)
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


        // Derived class from User--Inheritance.
        public class SportsmansChestDeveloper : Developer
        {
            public string DeveloperInterest { get; set; }

            public SportsmansChestDeveloper(string developerInterest, string name) : base(name)
            {
                this.DeveloperInterest = developerInterest;
            }

            // used the override access modifier to allow Polymorphism to be used.
            public override string ShowDetails()
            {
                StringBuilder details = new StringBuilder("Interest: " + DeveloperInterest + ";  Name: " + Name);

                return details.ToString();
            }
        }
    }
}
