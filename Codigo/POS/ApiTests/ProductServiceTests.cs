using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_Api.Services.Exceptions;
using Models;
using System.Linq;
using Rest_Api.Interfaces;
using Rest_Api.Services;

namespace ApiTests
{
    /*[TestClass]
    public class ProductServiceTests
    {
        private ProductService productService = new ProductService();
        private IGetService<Colour> _colourService = new ColourService();
        private IGetService<Brand> _brandService = new BrandService();
        private IGetService<Category> _categoryService = new CategoryService();

        [TestInitialize] public void TestInitialize() 
        {
            productService.BrandService = _brandService;
            productService.CategoryService = _categoryService;
            productService.ColourService = _colourService;
        }

        [TestMethod]
        public void GetAll_ReturnsListOfProducts()
        {
            var products = productService.GetAll();

            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count > 0);
        }

        [TestMethod]
        public void Get_ReturnsProductById()
        {
            var productId = 1;
            var product = productService.Get(productId);

            Assert.IsNotNull(product);
            Assert.AreEqual(productId, product.Id);
        }

        [TestMethod]
        public void Add_ProductIsAddedToList()
        {
            Brand brand = new Brand();
            brand.Name = "Puma";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var newProduct = new Product
            {
                Id = 3,
                Name = "Socks",
                PriceUYU = 200,
                Description = "Comfortable socks.",
                Brand = brand,
                Category = category,
                Colours = colours
            };

            productService.Add(newProduct);
            var addedProduct = productService.Get(newProduct.Id);

            Assert.IsNotNull(addedProduct);
            Assert.AreEqual(newProduct.Id, addedProduct.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ArgumentException))]
        public void Add_ProductFails()
        {
            Brand brand = new Brand();
            brand.Name = "404NOTFOUND";

            Category category = new Category();
            category.Name = "404NOTFOUND";

            Colour colour = new Colour();
            colour.Name = "404NOTFOUND";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var newProduct = new Product
            {
                Id = 3,
                Name = "Socks",
                PriceUYU = 200,
                Description = "Comfortable socks.",
                Brand = brand,
                Category = category,
                Colours = colours
            };

            productService.Add(newProduct);
            var addedProduct = productService.Get(newProduct.Id);
        }

        [TestMethod]
        public void Delete_ProductIsRemovedFromList()
        {
            var productIdToRemove = 1;
            var restoreProduct = productService.Get(productIdToRemove);
            productService.Delete(productIdToRemove);
            var deletedProduct = productService.Get(productIdToRemove);
            Assert.IsNull(deletedProduct);
        }

        [TestMethod]
        public void Update_ProductIsUpdated()
        {
            var productIdToUpdate = 1;

            Brand brand = new Brand();
            brand.Name = "Puma";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var updatedProduct = new Product
            {
                Id = 1,
                Name = "Updated Cap",
                PriceUYU = 800,
                Description = "Updated description.",
                Brand = brand,
                Category = category,
                Colours = colours
            };

            productService.Update(updatedProduct);
            var product = productService.Get(productIdToUpdate);

            Assert.IsNotNull(product);
            Assert.AreEqual(updatedProduct.Name, product.Name);
            Assert.AreEqual(updatedProduct.PriceUYU, product.PriceUYU);
            Assert.AreEqual(updatedProduct.Description, product.Description);
        }
    }*/
}
