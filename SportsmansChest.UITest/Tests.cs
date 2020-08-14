using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using SportsmansChest.Model;

namespace SportsmansChest.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        //my added test
        [Test]
        public void InventoryItem_AdditionTest()
        {
            // Arrange - Arrange part I have created an instance of Method Class which I have written in PCL project
            InventoryItem inventoryItem = new InventoryItem();
            int expectedResult = 16;

            // Act - ACT in this portion I am passing the value in method parameter.
            int actualResult = inventoryItem.Addititon(11,5);

            // Assert - Assert in this part you can see I am comparing values.
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
