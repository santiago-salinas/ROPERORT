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

            var mock = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mock.Setup(s => s.Get(1)).Returns(new Product { Id = 1, Name = "Cap1", PriceUYU = 600, Description = "Stylish Cap.", Brand = brand, Category = category, Colour = colour });
            var controller = new CartController(mock.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                id = 1,
                Quantity = 3
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as CreatedAtRouteResult;

            double expectedValue = 600 * 3;
            Assert.AreEqual(expectedValue, createdResult.Value);

        }

        [TestMethod]
        [ExpectedException(typeof(Models_ArgumentException))]
        public void CartControllerTestFailedNegativeQuantity()
        {
            Brand brand = new Brand();
            brand.Name = "Adidas";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            var mock = new Mock<ICRUDService<Product>>(MockBehavior.Strict);
            mock.Setup(s => s.Get(1)).Returns(new Product { Id = 1, Name = "Cap1", PriceUYU = 600, Description = "Stylish Cap.", Brand = brand, Category = category, Colour = colour });
            var controller = new CartController(mock.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                id = 1,
                Quantity = -1
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as CreatedAtRouteResult;

        }
    }
}
