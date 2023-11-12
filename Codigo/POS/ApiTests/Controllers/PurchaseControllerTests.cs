using Microsoft.AspNetCore.Http;
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
        private Mock<IPromoService> _mockDiscounts;
        private Mock<IPurchaseService> _mockPurchaseService;
        private Mock<ProductService> _mockProductsService;
        private Mock<IUserService> _mockUsersService;

        private PurchaseController _purchaseController;


        private User _testUser;
        private User _testUserTwo;

        private Cart _testCart;
        private Cart _testCartTwo;

        private Purchase _testPurchase;
        private Purchase _testPurchaseTwo;

        private DateTime _nowDate;

        List<Purchase> _purchaseHistory;
        List<User> _allUsers;

        [TestInitialize]
        public void TestInitialize()
        {
            _nowDate = DateTime.Now;
            _mockProductsService = new Mock<ProductService>(MockBehavior.Loose);
            _mockPurchaseService = new Mock<IPurchaseService>(MockBehavior.Loose);
            _mockUsersService = new Mock<IUserService>(MockBehavior.Loose);


            _testUser = new User("email1@gmail.com", "address1", "password") { Id = 1, Token = "unbuentoken" };
            _testUserTwo = new User("email2@gmail.com", "address2", "password") { Id = 2, Token = "unmaltoken" };
            _allUsers = new List<User>();
            _allUsers.Add(_testUser);
            _allUsers.Add(_testUserTwo);
            _mockUsersService.Setup(s => s.GetAll()).Returns(_allUsers);


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

            
            _purchaseHistory = new List<Purchase>();
            _purchaseHistory.Add(_testPurchase);

            _mockPurchaseService.Setup(s => s.GetPurchaseHistoryFromUser(1)).Returns(_purchaseHistory);
            _mockPurchaseService.Setup(s => s.Get(1)).Returns(_testPurchase);


            _purchaseController = new PurchaseController(_mockPurchaseService.Object, _mockUsersService.Object);
        }


        [TestMethod]
        public void GetPurchaseHistoryFromAuthToken()
        {
            
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            _purchaseController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
                        
            var result = _purchaseController.GetPurchaseHistory();
            var createdResult = result as ActionResult<List<Purchase>>;
            Assert.AreEqual(_purchaseHistory, createdResult.Value);
        }

        [TestMethod]
        public void GetPurchaseHistoryFromAuthTokenDenied()
        {

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unmaltoken";

            _purchaseController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = _purchaseController.GetPurchaseHistory();
            var createdResult = result as ActionResult<List<Purchase>>;
            Assert.AreEqual(null, createdResult.Value);
        }

        [TestMethod]
        public void GetPurchaseWithIdFromAuthToken()
        {

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            _purchaseController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = _purchaseController.Get(1);
            var createdResult = result as ActionResult<Purchase>;
            Assert.AreEqual(_testPurchase, createdResult.Value);
        }

        [TestMethod]
        public void GetPurchaseWithIdFromAuthTokenDenied()
        {

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unmaltoken";

            _purchaseController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = _purchaseController.Get(1);
            var createdResult = result as ActionResult<Purchase>;
            Assert.AreEqual(null, createdResult.Value);
        }

    }
}
