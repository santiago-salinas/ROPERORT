using Models;
using Rest_Api.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Exceptions;

namespace ApiTests.Models
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void EmptyCartPrice()
        {
            Cart cart = new Cart();

            double totalPrice = cart.PriceUYU;

            Assert.AreEqual(0, totalPrice);
        }

        [TestMethod]
        public void CartPrice()
        {
            Cart cart = new Cart();
            var product = new Product { Id = 1, Name = "Test Product", PriceUYU = 10.0 };
            var cartLine = new CartLine { Product = product, Quantity = 1 };
            cart.Products = new List<CartLine> { cartLine };

            double totalPrice = cart.PriceUYU;


            Assert.AreEqual(10.0, totalPrice);
        }

        [TestMethod]
        public void CartPriceMore()
        {
            Cart cart = new Cart();
            var product1 = new Product { Id = 1, Name = "Product 1", PriceUYU = 10.0 };
            var product2 = new Product { Id = 2, Name = "Product 2", PriceUYU = 15.0 };
            var cartLine1 = new CartLine { Product = product1, Quantity = 2 };
            var cartLine2 = new CartLine { Product = product2, Quantity = 3 };
            cart.Products = new List<CartLine> { cartLine1, cartLine2 };

            double totalPrice = cart.PriceUYU;

            Assert.AreEqual(65.0, totalPrice);
        }

        [TestMethod]
        [ExpectedException(typeof(Models_ArgumentException))]
        public void CartFailsNegativeQuantity()
        {
            Cart cart = new Cart();
            var product = new Product { Id = 1, Name = "Test Product", PriceUYU = 10.0 };
            var cartLine = new CartLine { Product = product, Quantity = -1 };
            cart.Products = new List<CartLine> { cartLine };

            double totalPrice = cart.PriceUYU;

            Assert.AreEqual(10.0, totalPrice);
        }
    }
}
