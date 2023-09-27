using Moq;
using Services;
using Services.Models;
using Services.Interfaces;

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

            _testCart = new Cart();
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

            _testUser = new User(1, "email1@gmail.com", "address1");
            _testUserTwo = new User(2, "email2@gmail.com", "address2");

            _testCart = new Cart();
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
    }
}
