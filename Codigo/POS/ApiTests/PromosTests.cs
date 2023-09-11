using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_Api.Models;
using Rest_Api.Models.Promos;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ApiTests
{
    [TestClass]
    public class TwentyPercentOffTests
    {
        private Product _testProduct1 = new Product()
        {
            Name = "Product1",
            PriceUYU = 10.0,
        };

        private Product _testProduct2 = new Product()
        {
            Name = "Product2",
            PriceUYU = 20.0,
        };

        private Product _testProduct3 = new Product()
        {
            Name = "Product3",
            PriceUYU = 15.0,
        };

        [TestMethod]
        public void ApplyDiscount_NoProducts_NoDiscount()
        {
            var promo = new TwentyPercentOff();
            var cart = new Cart();

            var discountedPrice = promo.ApplyDiscount(cart);

            Assert.AreEqual(0, discountedPrice);
        }

        [TestMethod]
        public void ApplyDiscount_OneProduct_NoDiscount()
        {
            var promo = new TwentyPercentOff();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });

            var discountedPrice = promo.ApplyDiscount(cart);

            Assert.AreEqual(10.0, discountedPrice);
        }

        [TestMethod]
        public void ApplyDiscount_TwoProducts_DiscountApplied()
        {
            var promo = new TwentyPercentOff();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });

            var discountedPrice = promo.ApplyDiscount(cart);

            Assert.AreEqual(26.0, discountedPrice); 
        }

        [TestMethod]
        public void ApplyDiscount_TwoOfTheSameProduct_DiscountApplied()
        {
            var promo = new TwentyPercentOff();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 2 });

            var discountedPrice = promo.ApplyDiscount(cart);

            Assert.AreEqual(36.0, discountedPrice);
        }

        [TestMethod]
        public void ApplyDiscount_ThreeProducts_DiscountAppliedToSecondCheapest()
        {
            var promo = new TwentyPercentOff();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct3, Quantity = 1 });

            var discountedPrice = promo.ApplyDiscount(cart);

            Assert.AreEqual(42.0, discountedPrice); 
        }
    }
}
