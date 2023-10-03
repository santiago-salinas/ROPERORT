using Moq;
using Services;
using Services.Models;
using Services.Interfaces;
using DataAccess.Repositories;
using Services.Exceptions;

namespace ApiTests.Services
{
    [TestClass]
    public class PurchaseServiceTests
    {
        private PurchaseService _purchaseService;
        private Mock<ICRUDRepository<Purchase>> _purchaseRepository;

        private User _testUser;
        private User _testUserTwo;

        private Cart _testCart;
        private Cart _testCartTwo;

        private Purchase _testPurchase;
        private Purchase _testPurchaseTwo;

        [TestInitialize]
        public void TestInitialize()
        {
            _purchaseRepository = new Mock<ICRUDRepository<Purchase>>();
            _purchaseService = new PurchaseService(_purchaseRepository.Object);

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

            _testUser = new User("email1@gmail.com", "address1", "password1")
            {
                Id = 1,
            };
            _testUserTwo = new User("email2@gmail.com", "address2", "password2")
            {
                Id = 2,
            };

            _testCart = new Cart();
            _testCartTwo = new Cart();

            _testPurchase = new Purchase
            {
                Id = 1,
                User = _testUser,
                Cart = _testCart,
                Date = DateTime.Now
            };

            _testPurchaseTwo = new Purchase
            {
                Id = 2,
                User = _testUserTwo,
                Cart = _testCartTwo,
                Date = DateTime.Now
            };
        }

        [TestMethod]
        public void GetAll_ReturnsListOfPurchases()
        {
            var purchaseList = new List<Purchase>
            {
                _testPurchase,
                _testPurchaseTwo                
            };
            _purchaseRepository.Setup(repo => repo.GetAll()).Returns(purchaseList);

            var purchases = _purchaseService.GetAll();

            Assert.IsNotNull(purchases);
            Assert.AreEqual(purchaseList.Count, purchases.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void GetAll_Fails_ThrowsException()
        {
            _purchaseRepository.Setup(r => r.GetAll()).Throws(new DatabaseException("Get all fails"));
            List<Purchase> purchases = _purchaseService.GetAll();
        }

        [TestMethod]
        public void Get_ReturnsPurchaseById()
        {
            var id = 1;
            var expectedPurchase = _testPurchase;
            _purchaseRepository.Setup(repo => repo.Get(id)).Returns(expectedPurchase);

            var purchase = _purchaseService.Get(id);

            Assert.IsNotNull(purchase);
            Assert.AreEqual(expectedPurchase.Id, purchase.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void Get_Fails_ThrowsException()
        {
            int purchaseId = 1;
            _purchaseRepository.Setup(r => r.Get(purchaseId)).Throws(new DatabaseException("Get all fails"));
            Purchase purchase = _purchaseService.Get(purchaseId);
        }

        [TestMethod]
        public void Add_PuchaseIsAddedToList()
        {
            var newPurchase = _testPurchase;
            _purchaseRepository.Setup(repo => repo.Add(newPurchase));
            _purchaseRepository.Setup(repo => repo.Get(newPurchase.Id)).Returns(newPurchase);

            _purchaseService.Add(newPurchase);
            var addedPurchase = _purchaseService.Get(newPurchase.Id);

            Assert.IsNotNull(addedPurchase);
            Assert.AreEqual(newPurchase.Id, addedPurchase.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void Add_Fails_ThrowsException()
        {
            var newPurchase = _testPurchase;
            _purchaseRepository.Setup(repo => repo.Add(newPurchase)).Throws(new DatabaseException("Add fails"));

            _purchaseService.Add(newPurchase);
        }

        [TestMethod]
        public void GetPurchaseHistory_ReturnsListOfPurchases()
        {
            var purchaseList = new List<Purchase>
            {
                _testPurchase,
                _testPurchaseTwo
            };
            _purchaseRepository.Setup(repo => repo.GetAll()).Returns(purchaseList);

            var purchases = _purchaseService.GetPurchaseHistoryFromUser(_testUser.Id);
            var expectedResult = new List<Purchase> { _testPurchase };

            Assert.IsNotNull(purchases);
            Assert.AreEqual(expectedResult.Count, purchases.Count);
            Assert.AreEqual(expectedResult.First(), _testPurchase);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void GetPurchaseHistoryl_Fails_ThrowsException()
        {
            _purchaseRepository.Setup(r => r.GetAll()).Throws(new DatabaseException("Get all fails"));
            List<Purchase> purchases = _purchaseService.GetPurchaseHistoryFromUser(_testUser.Id);
        }
    }
}
