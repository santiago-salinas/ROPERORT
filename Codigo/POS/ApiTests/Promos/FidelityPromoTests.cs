using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests
{
    [TestClass]
    public class FidelityPromoTests
    {
        private Product _testProduct1;
        private Product _testProduct2;
        private Product _testProduct3;
        private Product _testProduct4;
        private Brand brand = new Brand()
        {
            Name = "Vans",
        };
        private Brand otherBrand = new Brand()
        {
            Name = "Converse",
        };

        [TestInitialize]
        public void TestInit()
        {
            _testProduct1 = new Product()
            {
                Name = "Product1",
                PriceUYU = 10.0,
                Brand = brand
            };
            _testProduct2 = new Product()
            {
                Name = "Product2",
                PriceUYU = 20.0,
                Brand = brand
            };
            _testProduct3 = new Product()
            {
                Name = "Product3",
                PriceUYU = 15.0,
                Brand = brand
            };
            _testProduct4 = new Product()
            {
                Name = "Product4",
                PriceUYU = 25.0,
                Brand = otherBrand
            };
        }

        [TestMethod]
        public void ApplyDiscount_NoProducts_NoDiscount()
        {
            var promo = new FidelityPromo();
            var cart = new Cart();
            var expectedPrice = 0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void ApplyDiscount_OneProduct_NoDiscount()
        {
            var promo = new FidelityPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            var expectedPrice = 10.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void ThreeProducts_FidelityPromoGetsApplied()
        {
            var promo = new FidelityPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct3, Quantity = 1 });
            var expectedPrice = 20.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void ThreeProducts_DifferentCatgories_DoesntGetApplied()
        {
            var promo = new FidelityPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct4, Quantity = 1 });
            var expectedPrice = 55.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void FourProducts_SameBrands_GetsApplied()
        {
            var promo = new FidelityPromo();
            var cart = new Cart();
            _testProduct4.Brand = brand;
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct3, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct4, Quantity = 1 });
            var expectedPrice = 45.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void ThreeTimesSameProduct_GetsApplied()
        {
            var promo = new FidelityPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 3 });
            var expectedPrice = 10.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void MultipleTimesCheapProduct_OneExpensive_GetsApplied()
        {
            var promo = new FidelityPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 2 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });
            var expectedPrice = 20.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void MultipleTimesExpensiveProduct_OneCheap_GetsApplied()
        {
            var promo = new FidelityPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 2 });
            var expectedPrice = 20.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }
    }
}
