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

        //Testing on a method on a mac
        [Test]
        public void Test1()
        {
            // Arrange - Arrange part I have created an instance of Method Class which I have written in PCL project
            InventoryItem inventoryItem = new InventoryItem();
            int expectedResult = 16;

            // Act - ACT in this portion I am passing the value in method parameter.
            int actualResult = inventoryItem.Addititon(11, 5);

            // Assert - Assert in this part you can see I am comparing values.
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}