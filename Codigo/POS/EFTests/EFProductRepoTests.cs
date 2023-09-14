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

namespace EFTests

{
    [TestClass]
    public class EFProductRepositoryTests
    {
        private EFProductRepository _productRepository;
        private EFBrandService _brandService;
        private EFCategoryService _categoryService;
        private EFColourService _colourService;
        private Brand brand;
        private Colour colour;
        private Category category;
        private List<Colour> colours;
        

        [TestInitialize]
        public void TestInitialize()
        {
            _productRepository = new EFProductRepository();
            brand = new Brand();
            brand.Name = "Adidas";

            category = new Category();
            category.Name = "Shorts";

            colour = new Colour();
            colour.Name = "Red";

            colours = new List<Colour>
             {
                 colour
             };
            BrandEntity brandEntity = new BrandEntity();
            brandEntity.Name = "Adidas";

            CategoryEntity categoryEntity = new CategoryEntity();
            categoryEntity.Name = "Shorts";

            ColourEntity colourEntity = new ColourEntity();
            colourEntity.Name = "Red";

            List<ColourEntity> colourEntities = new List<ColourEntity>
            {
                colourEntity
            };
            using(EFContext dbContext = new EFContext())
            {
                dbContext.BrandEntities.Add(brandEntity);
                dbContext.CategoryEntities.Add(categoryEntity);
                dbContext.ColourEntities.Add(colourEntity);
                dbContext.SaveChanges();
            }

        }

        [TestCleanup]
        public void TestCleanup()
        {
            using (EFContext dbContext = new EFContext())
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
            }
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

        
    }

}
