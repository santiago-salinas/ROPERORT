﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Rest_Api.DTOs;
using Services.Interfaces;
using Services.Models;
using System.Drawing;

namespace ApiTests.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        private Mock<IPromoService> _mockDiscounts;
        private Mock<IProductService>  _mockProduct;
        private Mock<IPurchaseService> _mockPurchase;
        private Mock<IUserService> _mockUser;
        private Product _testProduct;
        private User _testUser;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDiscounts = new Mock<IPromoService>(MockBehavior.Strict);
            _mockPurchase = new Mock<IPurchaseService>(MockBehavior.Strict);
            _mockUser = new Mock<IUserService>(MockBehavior.Strict);
            _mockProduct = new Mock<IProductService>(MockBehavior.Strict);

            _mockDiscounts.Setup(s => s.GetAll()).Returns(new List<Promo>
            {
                new FidelityPromo(),
                new ThreeForTwoPromo(),
                new TwentyPercentOff(),
                new TotalLookPromo()
            });

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

            _testProduct = new Product()
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours,
                Stock = 10,
            };

           _testUser = new User("prueba@gmail.com", "Calle 123", "password") { Id = 5, Token = "unbuentoken" };

            _mockProduct.Setup(s => s.Get(_testProduct.Id)).Returns(_testProduct);
        }

        [TestMethod]
        public void CartControllerTestSuccess()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

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
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = -1
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Quantity cannot be less or equal than 0.", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailedZeroQuantity()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = -1
            };

            cartDto.Products.Add(cartLineDto);


            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Quantity cannot be less or equal than 0.", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailedBadProductId()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 2,
                Quantity = -1
            };
            _mockProduct.Setup(s => s.Get(cartLineDto.Id)).Returns(null as Product);

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Product id was not found", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailedEmptyCart()
        {
            var mockProduct = new Mock<IProductService>(MockBehavior.Loose);
            var controller = new CartController(mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Empty Cart", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void CartDiscountAppliedSuccess()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

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

        [TestMethod]
        public void BuyCartSuccessTest()
        {
            _testUser.AddRole(new Role() { Name = "Customer" });
            List<User> users = new List<User>();
            users.Add(_testUser);

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            var mockPurchase = new Mock<IPurchaseService>(MockBehavior.Loose);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };
            cartDto.Products.Add(cartLineDto);

            var result = controller.Buy(cartDto);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod] 
        public void BuyCartFailTestNotCustomer()
        {
            List<User> users = new List<User>();
            users.Add(_testUser);

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            var mockPurchase = new Mock<IPurchaseService>(MockBehavior.Loose);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            var result = controller.Buy(cartDto);
            var createdResult = result as ObjectResult;
            Assert.AreEqual(403, createdResult.StatusCode);
        }

        [TestMethod]
        public void BuyCartFailEmptyCart()
        {
            _testUser.AddRole(new Role() { Name = "Customer" });
            List<User> users = new List<User>();
            users.Add(_testUser);

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            var mockPurchase = new Mock<IPurchaseService>(MockBehavior.Loose);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            var result = controller.Buy(cartDto);
            var createdResult = result as BadRequestObjectResult;
            Assert.AreEqual("Empty Cart", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void BuyingMoreThanAvailableStock_Fails() 
        {
            User user = new User("prueba@gmail.com", "Calle 123", "password") { Id = 5, Token = "unbuentoken" };
            user.AddRole(new Role() { Name = "Customer" });
            List<User> users = new List<User>
            {
                user
            };
            Product product = new Product() { Id = 1, Stock = 2};

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            _mockProduct.Setup(s => s.Get(product.Id)).Returns(product);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };
            cartDto.Products.Add(cartLineDto);

            var result = controller.Buy(cartDto);
            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Not enough stock available to purchase " + product.Name, createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }
    }
}
