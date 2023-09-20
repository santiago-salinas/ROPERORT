using Services.Models;

namespace ApiTests.Models
{
    [TestClass]
    public class BoughtProductTests
    {
        private const int ValidQuantity = 1;
        private const int InvalidQuantity = 0;

        private Product someProduct = new Product();
        private BoughtProduct someBoughtProduct;

        [TestInitialize]
        public void TestInit()
        {
            someBoughtProduct = new BoughtProduct()
            {
                Product = someProduct,
                Quantity = ValidQuantity
            };
        }

        [TestMethod]
        public void CreateBoughtProductSuccessTest()
        {
            var boughtProduct = new BoughtProduct()
            {
                Product = someProduct,
                Quantity = ValidQuantity
            };
            Assert.IsNotNull(boughtProduct);
            Assert.AreEqual(someProduct.Name, boughtProduct.Product.Name);
            Assert.AreEqual(ValidQuantity, boughtProduct.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Quantity must be more than 0")]
        public void ThrowsExceptionWhenBuying0OrLessProducts()
        {
            someBoughtProduct.Quantity = InvalidQuantity;
        }
    }
}
