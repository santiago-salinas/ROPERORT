using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_Api.Models;
using Rest_Api.Services;
using System.Linq;

namespace ApiTests
{
    [TestClass]
    public class ProductServiceTests
    {
        ProductService productService = new ProductService();

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
            var newProduct = new Product { Id = 3, Name = "Socks", PriceUYU = 200, Description = "Comfortable socks.", Brand = "Adidas", Category = "Clothes", Colour = "Black" };

            productService.Add(newProduct);
            var addedProduct = productService.Get(newProduct.Id);

            Assert.IsNotNull(addedProduct);
            Assert.AreEqual(newProduct.Id, addedProduct.Id);
        }

        [TestMethod]
        public void Delete_ProductIsRemovedFromList()
        {
            var productIdToRemove = 1;
            var restoreProduct = productService.Get(productIdToRemove);
            productService.Delete(productIdToRemove);
            var deletedProduct = productService.Get(productIdToRemove);
            Assert.IsNull(deletedProduct);
            productService.Add(restoreProduct);
        }

        [TestMethod]
        public void Update_ProductIsUpdated()
        {
            var productIdToUpdate = 1;
            var updatedProduct = new Product { Id = 1, Name = "Updated Cap", PriceUYU = 800, Description = "Updated description.", Brand = "Nike", Category = "Clothes", Colour = "Blue" };

            productService.Update(updatedProduct);
            var product = productService.Get(productIdToUpdate);

            Assert.IsNotNull(product);
            Assert.AreEqual(updatedProduct.Name, product.Name);
            Assert.AreEqual(updatedProduct.PriceUYU, product.PriceUYU);
            Assert.AreEqual(updatedProduct.Description, product.Description);
        }
    }
}
