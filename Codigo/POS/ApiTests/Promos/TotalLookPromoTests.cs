using Services.Models;

namespace ApiTests.Promos
{
    [TestClass]
    public class TotalLookPromoTests
    {
        private Product _testProduct1;
        private Product _testProduct2;
        private Product _testProduct3;
        private Product _testProduct4;
        private Colour firstColour = new Colour()
        {
            Name = "Red",
        };
        private Colour secondColour = new Colour()
        {
            Name = "Green",
        };
        private Colour thirdColour = new Colour()
        {
            Name = "Blue",
        };

        [TestInitialize]
        public void TestInit()
        {
            _testProduct1 = new Product()
            {
                Name = "Product1",
                PriceUYU = 10.0,
                Colours = new List<Colour> { firstColour }
            };
            _testProduct2 = new Product()
            {
                Name = "Product2",
                PriceUYU = 20.0,
                Colours = new List<Colour> { firstColour, secondColour }
            };
            _testProduct3 = new Product()
            {
                Name = "Product3",
                PriceUYU = 15.0,
                Colours = new List<Colour> { thirdColour, secondColour }
            };
            _testProduct4 = new Product()
            {
                Name = "Product4",
                PriceUYU = 25.0,
                Colours = new List<Colour> { firstColour, thirdColour }
            };
        }

        [TestMethod]
        public void ApplyDiscount_NoProducts_NoDiscount()
        {
            var promo = new TotalLookPromo();
            var cart = new Cart();
            var expectedPrice = 0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void ApplyDiscount_OneProduct_NoDiscount()
        {
            var promo = new TotalLookPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            var expectedPrice = 10.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void ThreeProducts_TotalLookPromoGetsApplied()
        {
            var promo = new TotalLookPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct4, Quantity = 1 });


            var expectedPrice = 42.5;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void ThreeProducts_DifferentColours_DoesntGetApplied()
        {
            var promo = new TotalLookPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct3, Quantity = 1 });
            var expectedPrice = 45.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void FourProducts_SameColours_CheapeastToStore_GetsApplied()
        {
            var promo = new TotalLookPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 2 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 2 });

            var expectedPrice = 50;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void ThreeTimesSameProduct_GetsApplied()
        {
            var promo = new TotalLookPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 3 });
            var expectedPrice = 25.0;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }

        [TestMethod]
        public void Complex_ChepeastToStore_GetsApplied()
        {
            var promo = new TotalLookPromo();
            var cart = new Cart();
            cart.Products.Add(new CartLine { Product = _testProduct1, Quantity = 2 });
            cart.Products.Add(new CartLine { Product = _testProduct2, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct3, Quantity = 1 });
            cart.Products.Add(new CartLine { Product = _testProduct4, Quantity = 2 });

            var expectedPrice = cart.PriceUYU - 10f;
            var discountedPrice = promo.ApplyDiscount(cart);
            Assert.AreEqual(expectedPrice, discountedPrice);
        }
    }
}
