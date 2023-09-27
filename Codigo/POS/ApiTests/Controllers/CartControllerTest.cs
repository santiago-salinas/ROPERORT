using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Rest_Api.DTOs;
using Services.Interfaces;
using Services.Models;

namespace ApiTests.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        public Mock<IGetService<Promo>> mockDiscounts = new Mock<IGetService<Promo>>(MockBehavior.Strict);

        [TestInitialize]
        public void TestInitialize()
        {
            mockDiscounts.Setup(s => s.GetAll()).Returns(new List<Promo>
            {
            new FidelityPromo(),
            new ThreeForTwoPromo(),
            new TwentyPercentOff(),
            new TotalLookPromo()
            });
        }

        [TestMethod]
        public void CartControllerTestSuccess()
        {
            Brand brand = new Brand();
            brand.Name = "Adidas";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var mockProduct = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mockProduct.Setup(s => s.Get(1)).Returns(new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            });
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as CreatedAtActionResult;

            Cart createdCart = (Cart)createdResult.Value;
            double expectedValue = 600 * 3;
            Assert.AreEqual(expectedValue, createdCart.PriceUYU);

        }

        [TestMethod]
        public void FailedNegativeQuantity()
        {
            Brand brand = new Brand();
            brand.Name = "Adidas";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var mockProduct = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mockProduct.Setup(s => s.Get(1)).Returns(new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            });
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object);


            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = -1
            };

            cartDto.Products.Add(cartLineDto);


            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Quantity cannot be less than 0.", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);

        }

        [TestMethod]
        public void FailedZeroQuantity()
        {
            Brand brand = new Brand();
            brand.Name = "Adidas";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var mockProduct = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mockProduct.Setup(s => s.Get(1)).Returns(new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            });

            var controller = new CartController(mockProduct.Object, mockDiscounts.Object);


            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = -1
            };

            cartDto.Products.Add(cartLineDto);


            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Quantity cannot be less than 0.", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);

        }

        [TestMethod]
        public void FailedBadProductId()
        {
            Brand brand = new Brand();
            brand.Name = "Adidas";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var mockProduct = new Mock<ICRUDService<Product>>(MockBehavior.Loose);
            mockProduct.Setup(s => s.Get(1)).Returns(new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            });

            var controller = new CartController(mockProduct.Object, mockDiscounts.Object);


            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 2,
                Quantity = -1
            };

            cartDto.Products.Add(cartLineDto);


            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Product id was not found", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);

        }

        [TestMethod]
        public void FailedEmptyCart()
        {
            var mockProduct = new Mock<ICRUDService<Product>>(MockBehavior.Loose);
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object);


            CartDTO cartDto = new CartDTO();

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Empty Cart", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);

        }

        [TestMethod]
        public void CartDiscountAppliedSuccess()
        {
            Brand brand = new Brand();
            brand.Name = "Adidas";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var mockProduct = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mockProduct.Setup(s => s.Get(1)).Returns(new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            });
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object);


            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as CreatedAtActionResult;

            Cart createdCart = (Cart)createdResult.Value;
            double originalValue = 600;
            double expectedValue = 600;
            Assert.AreEqual(expectedValue, createdCart.DiscountedPriceUYU);

        }
    }
}
