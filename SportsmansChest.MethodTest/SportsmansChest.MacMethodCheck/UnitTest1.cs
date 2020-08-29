using NUnit.Framework;
using SportsmansChest.Model;

namespace SportsmansChest.MacMethodCheck
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Testing for Polymorphism output on my mac
        [Test]
        public void Test1()
        {
            // Arrange
            StatisticsReport statistics = new StatisticsReport();
            string expectedResult = "Interest: Hunting;  Name: Joshua";

            // Act
            string actualResult = StatisticsReport.GetUserInfo();

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}