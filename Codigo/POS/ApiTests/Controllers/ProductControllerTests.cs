﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;


namespace ApiTests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        private Mock<IProductService> _mock;
        private ProductController _productController;

        [TestInitialize]
        public void TestInitialize()
        {
            _mock = new Mock<IProductService>(MockBehavior.Strict);
            _productController = new ProductController(_mock.Object);
        }

        [TestMethod]
        public void GetAllWorks_ReturnsAllProducts()
        {
            List<Product> expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product1" },
                new Product { Id = 2, Name = "Product2" },
            };
            _mock.Setup(s => s.GetAll()).Returns(expectedProducts);
            var result = _productController.GetAll();
            var createdResult = result;
            Assert.AreEqual(expectedProducts.Count, createdResult.Value.Count);
        }

        [TestMethod]
        public void GivenValidId_GetReturnsProduct()
        {
            int productId = 10;
            Product expectedOutcome = new Product()
            {
                Id = productId,
                Name = "Product A",
            };
            _mock.Setup(s => s.Get(productId)).Returns(expectedOutcome);
            var result = _productController.Get(productId);
            var createdResult = result;
            Assert.AreEqual(expectedOutcome, createdResult.Value);
        }

        [TestMethod]
        public void GivenInvalidId_GetReturnsNotFound()
        {
            Product? nullUser = null;
            _mock.Setup(s => s.Get(7)).Returns(nullUser);
            var result = _productController.Get(7);
            Assert.AreEqual(result.Value, null);
        }

        [TestMethod]
        public void GivenValidProduct_ItGetsCreated()
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

            Product testProduct = new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            };

            _mock.Setup(s => s.Add(testProduct));

            var result = _productController.Create(testProduct);
            var createdResult = result as OkResult;
            Assert.AreEqual(createdResult.StatusCode, 200);
        }

        [TestMethod]
        public void GivenInvalidProduct_CreateReturnsBadRequest()
        {
            Product testProduct = new Product
            {
                Id = 11,
                Name = "Cap1",
                PriceUYU = 600,
            };
            _mock.Setup(s => s.Add(testProduct)).Throws(new Service_ObjectHandlingException(""));
            var result = _productController.Create(testProduct);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GivenCorrectValues_ProductGetsUpdated()
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
            Product testProduct = new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            };
            _mock.Setup(s => s.Get(testProduct.Id)).Returns(testProduct);
            _mock.Setup(s => s.Update(testProduct));
            var result = _productController.Update(testProduct.Id, testProduct);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public void GivenDifferentId_UpdateReturnsBadRequest()
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
            Product testProduct = new Product
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours
            };
            var result = _productController.Update(5, testProduct);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GivenIdNotInDatabase_UpdateReturnsNotFound()
        {
            Product testProduct = new Product
            {
                Id = 11,
                Name = "Cap1",
                PriceUYU = 600,
            };
            Product? nullProduct = null;
            _mock.Setup(s => s.Get(testProduct.Id)).Returns(nullProduct);
            var result = _productController.Update(testProduct.Id, testProduct);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GivenValidIdProductGetsDeleted()
        {
            Product testProduct = new Product
            {
                Id = 11,
                Name = "Cap1",
                PriceUYU = 600,
            };
            _mock.Setup(s => s.Get(testProduct.Id)).Returns(testProduct);
            _mock.Setup(s => s.Delete(testProduct.Id));
            var result = _productController.Delete(testProduct.Id);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GivenNonExistentIdDeleteReturnsNotFound()
        {
            Product? nullProduct = null;
            _mock.Setup(s => s.Get(0)).Returns(nullProduct);
            var result = _productController.Delete(0);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
