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
                Price = 15.0,
            };
            List<ProductColors> productColours = new List<ProductColors>
            {
                new ProductColors(_testProduct1, colour, _context),
                new ProductColors(_testProduct2, colour, _context)
            };
            _testProduct1.Colours.Add(productColours[0]);
            _testProduct2.Colours.Add(productColours[1]);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            /*using (EFContext dbContext = new EFContext())
            {
                List<ProductEntity> products = dbContext.ProductEntities.Include(p => p.Colours).ToList();
                List<ProductColors> productColors = dbContext.ProductColors.ToList();
                List<BrandEntity> brands = dbContext.BrandEntities.ToList();
                List<CategoryEntity> categories = dbContext.CategoryEntities.ToList();
                List<ColourEntity> colours= dbContext.ColourEntities.ToList();

                dbContext.ProductColors.RemoveRange(productColors);
                dbContext.ProductEntities.RemoveRange(products);
                dbContext.BrandEntities.RemoveRange(brands);
                dbContext.ColourEntities.RemoveRange(colours);
                dbContext.CategoryEntities.RemoveRange(categories);
                dbContext.SaveChanges();
            }*/
            _context.Database.EnsureDeleted();
            _context.Dispose();
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

        private int AddTestData()
        {
            _context.ProductEntities.AddRange(_testProduct1, _testProduct2);
            _context.SaveChanges();

            return _testProduct1.Id; 
        }


    }

}
