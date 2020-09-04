using NUnit.Framework;
using SportsmansChest.Model;

namespace SportsmansChest.GetUserInfoTest
{
    public class GetUserInfoPolyTest
    {
        [SetUp]
        public void Setup()
        {
        }

        //Testing for GetUserInfo() Polymorphism output
        [Test]
        public void Test1()
        {
            // Arrange
            string expectedResult = "Interest: Hunting;  Name: Joshua";

            // Act
            string actualResult = StatisticsReport.GetUserInfo();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}