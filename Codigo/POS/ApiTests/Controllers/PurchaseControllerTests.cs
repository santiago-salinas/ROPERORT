using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Services;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;

namespace ApiTests.Controllers
{
    [TestClass]
    public class PurchaseControllerTest
    {
        private Mock<IPurchaseService> mock;
        private PurchaseController _purchaseController;
        private User _testUser;
        private User _testUserTwo;

        private Cart _testCart;
        private Cart _testCartTwo;

        private Purchase _testPurchase;
        private Purchase _testPurchaseTwo;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IPurchaseService>(MockBehavior.Strict);
            _purchaseController = new PurchaseController(mock.Object);

            Product product1 = new Product();
            product1.PriceUYU = 10.0;

            Product product2 = new Product();
            product2.PriceUYU = 15.0;

            var promo = new TwentyPercentOff();
            CartLine cartLine1 = new CartLine();
            cartLine1.Product = product1;
            cartLine1.Quantity = 2;

            CartLine cartLine2 = new CartLine();
            cartLine2.Product = product2;
            cartLine2.Quantity = 3;

            _testCart = new Cart();

            _testCart.Products.Add(cartLine1);
            _testCart.Products.Add(cartLine2);

            _testCart.AppliedPromo = promo;

            _testUser = new User("email1@gmail.com", "address1", "password") { Id = 1 };
            _testUserTwo = new User("email2@gmail.com", "address2", "password") { Id = 2 };

             _testCartTwo = new Cart();

            _testPurchase = new Purchase
            {
                Id = 1,
                Client = _testUser,
                Products = _testCart,
                Date = DateTime.Now,
                AppliedPromotion = "Promo1"
            };

            _testPurchaseTwo = new Purchase
            {
                Id = 2,
                Client = _testUserTwo,
                Products = _testCartTwo,
                Date = DateTime.Now,
                AppliedPromotion = "Promo2"
            };
        }

        [TestMethod]
        public void GivenValidId_GetReturnsUser()
        {
            var expectedOutcome = _testPurchase;
            mock.Setup(s => s.Get(5)).Returns(expectedOutcome);
            var result = _purchaseController.Get(5);
            var createdResult = result as ActionResult<Purchase>;
            Assert.AreEqual(expectedOutcome, createdResult.Value);
        }

        [TestMethod]
        public void GivenInvalidId_GetReturnsNotFound()
        {
            Purchase? nullPurchase = null;
            mock.Setup(s => s.Get(7)).Returns(nullPurchase);
            var result = _purchaseController.Get(7);
            Assert.AreEqual(result.Value, null);
        }

        [TestMethod]
        public void GivenValidPurchase_ItGetsCreated()
        {
            Purchase purchase = _testPurchase;
            mock.Setup(s => s.Add(purchase));
            var result = _purchaseController.Create(purchase);
            var createdResult = result as OkResult;
            Assert.AreEqual(createdResult.StatusCode, 200);
        }

        [TestMethod]
        public void GivenInvalidPurchase_Create_ReturnsBadRequest()
        {
            var purchase = _testPurchase;
            mock.Setup(s => s.Add(purchase)).Throws(new Service_ObjectHandlingException(""));
            var result = _purchaseController.Create(purchase);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GivenValidEmail_GetPurchaseHistory_ReturnsPurchasesFromGivenUser()
        {
            Purchase purchaseUserOne = new Purchase
            {
                Id = 2,
                Client = _testUser,
                Products = _testCartTwo,
                Date = DateTime.Now,
                AppliedPromotion = "Promo2"
            };
            Purchase purchaseUserTwo = new Purchase
            {
                Id = 2,
                Client = _testUserTwo,
                Products = _testCartTwo,
                Date = DateTime.Now,
                AppliedPromotion = "Promo2"
            };

            List<Purchase> getAll = new List<Purchase>() { purchaseUserOne, purchaseUserTwo };
            List<Purchase> purchaseHistory = new List<Purchase>() { purchaseUserOne };
            mock.Setup(s => s.GetPurchaseHistory(_testUser.Address)).Returns(purchaseHistory);
            var result = _purchaseController.GetPurchaseHistory(_testUser.Address);
            var createdResult = result as ActionResult<List<Purchase>>;
            Assert.AreEqual(purchaseHistory, createdResult.Value);
        }

    }
}
