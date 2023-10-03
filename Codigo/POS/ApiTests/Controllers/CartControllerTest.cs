﻿using Microsoft.AspNetCore.Mvc;
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
        private Mock<IPromoService> mockDiscounts;
        private Mock<IProductService>  mockProduct;
        private Mock<IPurchaseService> mockPurchase;
        private Mock<IUserService> mockUser;


        [TestInitialize]
        public void TestInitialize()
        {
            mockDiscounts = new Mock<IPromoService>(MockBehavior.Strict);
            mockPurchase = new Mock<IPurchaseService>(MockBehavior.Strict);
            mockUser = new Mock<IUserService>(MockBehavior.Strict);
            mockProduct = new Mock<IProductService>(MockBehavior.Loose);

            mockDiscounts.Setup(s => s.GetAll()).Returns(new List<Promo>
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
        }

        [TestMethod]
        public void CartControllerTestSuccess()
        {
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object, mockPurchase.Object, mockUser.Object);

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
            
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object, mockPurchase.Object, mockUser.Object);


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
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object, mockPurchase.Object, mockUser.Object);


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
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object, mockPurchase.Object, mockUser.Object);

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
            var mockProduct = new Mock<IProductService>(MockBehavior.Loose);
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object, mockPurchase.Object, mockUser.Object);


            CartDTO cartDto = new CartDTO();

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Empty Cart", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);

        }

        [TestMethod]
        public void CartDiscountAppliedSuccess()
        {
            var controller = new CartController(mockProduct.Object, mockDiscounts.Object, mockPurchase.Object, mockUser.Object);


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
