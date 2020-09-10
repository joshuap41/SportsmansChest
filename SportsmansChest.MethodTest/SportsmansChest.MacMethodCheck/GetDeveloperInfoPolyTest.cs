using NUnit.Framework;
using SportsmansChest.Model;

namespace SportsmansChest.GetDeveloperInfoTest
{
    public class GetDeveloperInfoPolyTest
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
            string expectedResult = "Interest: Hunting; Name: Joshua";

            // Act
            string actualResult = StatisticsReport.GetDeveloperInfo();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}