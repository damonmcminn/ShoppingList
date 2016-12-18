using System;
using System.Linq;
using NUnit.Framework;
using ShoppingListApi.Lib;
using ShoppingListApi.Models;

namespace ShoppingListApi.Test
{
    [TestFixture]
    public class ShoppingListTests
    {
        [Test, Order(1)]
        public void Add()
        {
            var pepsi = new Drink(" Pepsi");
            var capitalPepsi = new Drink("PEPSI");
            var paddedPepsi = new Drink("     pepsi       ");

            Assert.AreEqual(pepsi.Id, capitalPepsi.Id);
            Assert.AreEqual(capitalPepsi.Id, paddedPepsi.Id);

            Assert.True(ShoppingList.Add(pepsi));
            Assert.False(ShoppingList.Add(capitalPepsi));
            Assert.False(ShoppingList.Add(paddedPepsi));

            Assert.AreEqual("Pepsi", ShoppingList.Items.First().Name);
        }

        [Test, Order(99)]
        public void Remove()
        {
            var pepsi = new Drink("Pepsi");
            var nonExistentCokeId = new Drink("Coke").Id;

            Assert.True(ShoppingList.Remove(pepsi.Id));
            Assert.False(ShoppingList.Remove(nonExistentCokeId));
        }

        [Test, Order(2)]
        public void FindById()
        {
            var pepsi = new Drink("Pepsi                  ");
            var coke = new Drink("Coca Cola");

            var foundPepsi = ShoppingList.FindById(pepsi.Id);
            var notFoundCoke = ShoppingList.FindById(coke.Id);

            Assert.NotNull(foundPepsi);
            Assert.AreEqual(pepsi.Id, foundPepsi.Id);
            Assert.IsNull(notFoundCoke);
        }

        [Test, Order(3)]
        public void Update()
        {
            var updatedPepsi = new Drink("Pepsi");
            var result = ShoppingList.Update(updatedPepsi.Id, 42);

            var foundPepsi = ShoppingList.FindById(updatedPepsi.Id);

            Assert.True(result.Item1);
        }

        [Test, Order(4)]
        public void FindByName()
        {
            var foundPepsi = ShoppingList.FindByName("    pepsi");
            var notFoundCoke = ShoppingList.FindByName("coke");

            Assert.NotNull(foundPepsi);
            Assert.Null(notFoundCoke);
        }
    }
}