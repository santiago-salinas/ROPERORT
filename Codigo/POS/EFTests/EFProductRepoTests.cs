using DataAccess.Entities;
using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterfaces;
using System.Drawing;
using DataAccess.DatabaseServices;
using Rest_Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataAccess.Expcetions;

namespace EFTests

{
    [TestClass]
    public class EFProductRepositoryTests
    {
        private EFContext _context;

        private EFProductRepository _productRepository;
        private Brand brand;
        private Colour colour;
        private Category category;
        private List<Colour> colours;

        private ProductEntity _testProduct1;
        private ProductEntity _testProduct2;

        [TestInitialize]
        public void TestInitialize()
        {
            BrandEntity brandEntity = new BrandEntity();
            brandEntity.Name = "Adidas";

            CategoryEntity categoryEntity = new CategoryEntity();
            categoryEntity.Name = "Shorts";

            ColourEntity colourEntity = new ColourEntity();
            colourEntity.Name = "Red";

            var options = new DbContextOptionsBuilder<EFContext>()
                            .UseInMemoryDatabase(databaseName: "TestDatabase")
                            .Options;

            _context = new EFContext(options, true);
            _context.BrandEntities.Add(brandEntity);
            _context.CategoryEntities.Add(categoryEntity);
            _context.ColourEntities.Add(colourEntity);
            _context.SaveChanges();

            _productRepository = new EFProductRepository(_context);

            brand = new Brand();
            brand.Name = "Adidas";

            category = new Category();
            category.Name = "Shorts";

            colour = new Colour();
            colour.Name = "Red";

            colours = new List<Colour>{colour};

            _testProduct1 = new ProductEntity()
            {
                Brand = brandEntity,
                Category = categoryEntity,
                Description = "Description 1",
                Name = "Name 1",
                Price = 10.0,
            };
            _testProduct2 = new ProductEntity()
            {
                Brand = brandEntity,
                Category = categoryEntity,
                Description = "Description 2",
                Name = "Name 2",
                Price = 20.0,
            };
            List<ProductColors> productColours = new List<ProductColors>
            {
                new ProductColors(_testProduct1, colour, _context),
                new ProductColors(_testProduct2, colour, _context)
            };
            _testProduct1.Colours.Add(productColours[0]);
            _testProduct2.Colours.Add(productColours[1]);
        }

        

        [TestMethod]
        public void AddNewProducts()
        {
            var product1 = new Product { Id = 1, Name = "Product 1", Description = "Description 1", PriceUYU = 10.0, Brand = brand, Category = category, Colours = colours };
            var product2 = new Product { Id = 2, Name = "Product 2", Description = "Description 2", PriceUYU = 15.0, Brand = brand, Category = category, Colours = colours };

            _productRepository.Add(product1);
            _productRepository.Add(product2);

            List<Product> products
                = _productRepository.GetAll();

            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllProducts()
        {
            AddTestData();

            List<Product> products = _productRepository.GetAll();

            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public void Get_ShouldReturnProductById()
        {
            int productId = AddTestData();

            Product product = _productRepository.Get(productId);

            Assert.IsNotNull(product);
            Assert.AreEqual(productId, product.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void Add_ShouldThrowExceptionOnFailure()
        {
            Product invalidProduct = new Product { Name = "Invalid Product" };

            _productRepository.Add(invalidProduct);
        }

        [TestMethod]
        public void Delete_ShouldRemoveProduct()
        {
            int productId = AddTestData();

            _productRepository.Delete(productId);

            Product deletedProduct = _productRepository.Get(productId);
            Assert.IsNull(deletedProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseException))]
        public void Update_ShouldThrowExceptionOnFailure()
        {
            Product invalidProduct = new Product { Id = 999, Name = "Invalid Product" };

            _productRepository.Update(invalidProduct);
        }

        [TestMethod]
        public void GetAll_WithFilterByPrice()
        {
            AddFilterTestData();

            Func<ProductEntity, bool> filter = p => p.Price >= 50;

            List<Product> products = _productRepository.GetAll(filter);

            Assert.AreEqual(3, products.Count);

            Assert.IsTrue(products.All(p => p.PriceUYU >= 50));
        }

        [TestMethod]
        public void GetAll_FilterByColour_ShouldReturnFilteredProducts()
        {
            AddFilterTestData();

            Func<ProductEntity, bool> filter = p => p.Colours.Any(c => c.Colour.Name == "Green");

            List<Product> products = _productRepository.GetAll(filter);

            Assert.AreEqual(3, products.Count);

            Assert.IsTrue(products.All(p => p.Colours.Any(c => c.Name == "Green")));
        }

        [TestMethod]
        public void GetAll_FilterByBrand_ShouldReturnFilteredProducts()
        {
            AddFilterTestData();

            Func<ProductEntity, bool> filter = p => p.Brand.Name == "Puma";

            List<Product> products = _productRepository.GetAll(filter);

            Assert.AreEqual(2, products.Count);

            Assert.IsTrue(products.All(p => p.Brand.Name == "Puma"));
        }

        [TestMethod]
        public void GetAll_FilterByCategory_ShouldReturnFilteredProducts()
        {
            AddFilterTestData();

            Func<ProductEntity, bool> filter = p => p.Category.Name == "T-Shirt";

            List<Product> products = _productRepository.GetAll(filter);

            Assert.AreEqual(3, products.Count);

            Assert.IsTrue(products.All(p => p.Category.Name == "T-Shirt"));
        }


        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        private int AddTestData()
        {
            _context.ProductEntities.AddRange(_testProduct1, _testProduct2);
            _context.SaveChanges();

            return _testProduct1.Id; 
        }
        private void AddFilterTestData()
        {
            AddTestData();
            ColourEntity colourEntity = new ColourEntity();
            colourEntity.Name = "Green";
            ColourEntity colourEntity2 = new ColourEntity();
            colourEntity2.Name = "Blue";

            CategoryEntity categoryEntity = new CategoryEntity();
            categoryEntity.Name = "Pants";
            CategoryEntity categoryEntity2 = new CategoryEntity();
            categoryEntity2.Name = "T-Shirt";

            BrandEntity brandEntity = new BrandEntity();
            brandEntity.Name = "Puma";
            BrandEntity brandEntity2 = new BrandEntity();
            brandEntity2.Name = "Nike";

            _context.BrandEntities.Add(brandEntity);
            _context.CategoryEntities.Add(categoryEntity);
            _context.ColourEntities.Add(colourEntity);
            _context.BrandEntities.Add(brandEntity2);
            _context.CategoryEntities.Add(categoryEntity2);
            _context.ColourEntities.Add(colourEntity2);
            _context.SaveChanges();

            var product1 = new Product
            {
                Name = "Name 3",
                Description = "Description 3",
                PriceUYU = 20.0,
                Brand = new Brand("Nike"),
                Category = new Category("T-Shirt"),
                Colours = new List<Colour>() { new Colour("Blue")}
            };
            var product2 = new Product
            {
                Name = "Name 4",
                Description = "Description 4",
                PriceUYU = 50.0,
                Brand = new Brand("Puma"),
                Category = new Category("Pants"),
                Colours = new List<Colour>() { new Colour("Green"), new Colour("Red") }
            };
            var product3 = new Product
            {
                Name = "Name 5",
                Description = "Description 5",
                PriceUYU = 20.0,
                Brand = new Brand("Nike"),
                Category = new Category("T-Shirt"),
                Colours = new List<Colour> { new Colour("Red") }
            };

            var product4 = new Product
            {
                Name = "Name 6",
                Description = "Description 6",
                PriceUYU = 100.0,
                Brand = new Brand("Puma"),
                Category = new Category("Pants"),
                Colours = new List<Colour> { new Colour("Green"), new Colour("Red") }
            };

            var product5 = new Product
            {
                Name = "Name 7",
                Description = "Description 7",
                PriceUYU = 60.0,
                Brand = new Brand("Nike"),
                Category = new Category("T-Shirt"),
                Colours = new List<Colour> { new Colour("Blue"), new Colour("Green") }
            };

            _productRepository.Add(product1);
            _productRepository.Add(product2);
            _productRepository.Add(product3);
            _productRepository.Add(product4);
            _productRepository.Add(product5);
        }


    }

}
