using GroceryStore.Models;
using GroceryStore.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GroceryStore.Tests.Services
{
    [TestClass]
    public class GroceryServiceTests
    {
        [TestMethod]
        public void GroceryService_OldEnoughToDrink_OldEnough()
        {
            // Arrange
            GroceryService groceryService = new GroceryService(new ApplicationDbContext());
            DateTime oldEnough = DateTime.Now.AddYears(-20);
            bool expectedResult = true;

            // Act
            bool actualResult = groceryService.OldEnoughToDrink(oldEnough);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GroceryService_OldEnoughToDrink_NotOldEnough()
        {
            // Arrange
            GroceryService groceryService = new GroceryService(new ApplicationDbContext());
            DateTime notOldEnough = DateTime.Now.AddYears(-4);
            bool expectedResult = false;

            // Act
            bool actualResult = groceryService.OldEnoughToDrink(notOldEnough);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GroceryService_OldEnoughToDrink_FarFuture()
        {
            // Arrange
            GroceryService groceryService = new GroceryService(new ApplicationDbContext());
            DateTime notOldEnough = DateTime.Now.AddYears(20);
            bool expectedResult = false;

            // Act
            bool actualResult = groceryService.OldEnoughToDrink(notOldEnough);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
