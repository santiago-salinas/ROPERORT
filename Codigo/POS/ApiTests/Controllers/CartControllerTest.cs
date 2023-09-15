using Rest_Api.Models;
using Rest_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_Api.Controllers;
using System.Drawing;
using Rest_Api.DTOs;
using Rest_Api.Services.Exceptions;
using Rest_Api.Models.Exceptions;

namespace ApiTests
{
    [TestClass]
    public class CartControllerTest
    {
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

            var mock = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mock.Setup(s => s.Get(1)).Returns(new Product { Id = 1, Name = "Cap1", 
                                                            PriceUYU = 600, 
                                                            Description = "Stylish Cap.", 
                                                            Brand = brand, 
                                                            Category = category, 
                                                            Colours = colours });
            var controller = new CartController(mock.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                id = 1,
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

            var mock = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mock.Setup(s => s.Get(1)).Returns(new Product { Id = 1, 
                                                            Name = "Cap1", PriceUYU = 600, 
                                                            Description = "Stylish Cap.", 
                                                            Brand = brand, Category = category, 
                                                            Colours = colours });
            var controller = new CartController(mock.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                id = 1,
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

            var mock = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mock.Setup(s => s.Get(1)).Returns(new Product { Id = 1, 
                                                            Name = "Cap1", 
                                                            PriceUYU = 600, 
                                                            Description = "Stylish Cap.", 
                                                            Brand = brand, 
                                                            Category = category, 
                                                            Colours = colours });
            
            var controller = new CartController(mock.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                id = 1,
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

            var mock = new Mock<ICRUDService<Product>>(MockBehavior.Loose);
            mock.Setup(s => s.Get(1)).Returns(new Product { Id = 1, 
                                                            Name = "Cap1", PriceUYU = 600, 
                                                            Description = "Stylish Cap.", 
                                                            Brand = brand, 
                                                            Category = category, 
                                                            Colours = colours });

            var controller = new CartController(mock.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                id = 2,
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
            var mock = new Mock<ICRUDService<Product>>(MockBehavior.Loose);
            var controller = new CartController(mock.Object);

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
            
            // 3 productos -> 20% highest (1680)
            // 3 mismo color -> 1500
            // 3 misma categoria -> 1200
            // 3 misma brand -> 600
            var mock = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mock.Setup(s => s.Get(1)).Returns(new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            });
            var controller = new CartController(mock.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                id = 1,
                Quantity = 3
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as CreatedAtActionResult;

            Cart createdCart = (Cart)createdResult.Value;
            double originalValue = 600;
            double expectedValue = 600;
            Assert.AreEqual(expectedValue, createdCart.PriceUYU);

        }
    }
}
