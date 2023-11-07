using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Rest_Api.DTOs;
using Services;
using Services.Interfaces;
using Services.Models;
using Services.Models.PaymentMethods;
using System.Drawing;

namespace ApiTests.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        private Mock<IPromoService> _mockDiscounts;
        private Mock<IProductService>  _mockProduct;
        private Mock<IPurchaseService> _mockPurchase;
        private Mock<IUserService> _mockUser;
        private Product _testProduct;
        private Product _testProduct2;

        private User _testUser;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDiscounts = new Mock<IPromoService>(MockBehavior.Strict);
            _mockPurchase = new Mock<IPurchaseService>(MockBehavior.Strict);
            _mockUser = new Mock<IUserService>(MockBehavior.Strict);
            _mockProduct = new Mock<IProductService>(MockBehavior.Strict);

            PromoService service = new PromoService();
            service.GetAll();
            List<IPromo> promos = new List<IPromo>()
            {
                new FidelityPromo(),
                new ThreeForTwoPromo(),
                new TwentyPercentOff(),
                new TotalLookPromo(),
            };
            _mockDiscounts.Setup(s => s.GetAll()).Returns(promos) ;

            Brand brand = new Brand();
            brand.Name = "Adidas";

            Category category = new Category();
            category.Name = "Shorts";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<IColour> colours = new List<IColour>
            {
                colour
            };

            _testProduct = new Product()
            {
                Id = 1,
                Name = "Cap1",
                PriceUYU = 600,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours,
                Stock = 10,
            };
          
          _testProduct2 = new Product()
            {
                Id = 10,
                Name = "Cap2",
                PriceUYU = 6000,
                Description = "Stylish Cap.",
                Brand = brand,
                Category = category,
                Colours = colours,
                Stock = 100,
                Exclude = true,
            };

           _testUser = new User("prueba@gmail.com", "Calle 123", "password") { Id = 5, Token = "unbuentoken" };

            _mockProduct.Setup(s => s.Get(_testProduct.Id)).Returns(_testProduct);
            _mockProduct.Setup(s => s.Get(_testProduct2.Id)).Returns(_testProduct2);
        }
      

        [TestMethod]
        public void CartControllerTestSuccess()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as CreatedAtActionResult;

            Cart createdCart = (Cart)createdResult.Value;
            double expectedValue = 600 * 3;
            Assert.AreEqual(expectedValue, createdCart.PriceUYU);
        }

        [TestMethod]
        public void FailedNegativeQuantity()
        {            
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = -1
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Quantity cannot be less or equal than 0.", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailedZeroQuantity()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = -1
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Quantity cannot be less or equal than 0.", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailedBadProductId()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 2,
                Quantity = -1
            };
            _mockProduct.Setup(s => s.Get(cartLineDto.Id)).Returns(null as Product);

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Product id was not found", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void FailedEmptyCart()
        {
            var mockProduct = new Mock<IProductService>(MockBehavior.Loose);
            var controller = new CartController(mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();

            var result = controller.Create(cartDto);

            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Empty Cart", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void Failed_TryToAddMoreStockThanAvailable_ReturnsAllAvailableStock()
        {
            _mockProduct.Setup(s => s.Get(_testProduct.Id)).Returns(_testProduct);

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = _testProduct.Id,
                Quantity = 50
            };
            cartDto.PaymentMethod = "DEBIT";
            cartDto.Bank = "SANTANDER";
            cartDto.Products.Add(cartLineDto);
            
            var result = controller.Create(cartDto);
            var createdResult = result as ObjectResult;

            Cart expectedCart = new Cart()
            {
                Products = new List<ICartLine>()
                {
                    new CartLine() {
                        Product = _testProduct,
                        Quantity = _testProduct.Stock,
                    }
                }
            };
            var expectedResult = new ObjectResult(expectedCart)
            {
                StatusCode = 420
            };

            Assert.AreEqual(420, createdResult.StatusCode);
        }

        [TestMethod]
        public void CartDiscountAppliedSuccess()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as CreatedAtActionResult;

            Cart createdCart = (Cart)createdResult.Value;
            double expectedValue = 600;
            Assert.AreEqual(expectedValue, createdCart.DiscountedPriceUYU);
        }

        [TestMethod]
        public void BuyCartSuccessTest()
        {
            _testUser.AddRole(new Role() { Name = "Customer" });
            List<User> users = new List<User>();
            users.Add(_testUser);

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            _mockProduct.Setup(s => s.Update(It.IsAny<Product>()));
            var mockPurchase = new Mock<IPurchaseService>(MockBehavior.Loose);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };
            cartDto.Products.Add(cartLineDto);
            cartDto.PaymentMethod = "Paypal";
            cartDto.PaymentId = "ValidID";

            var result = controller.Buy(cartDto);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod] 
        public void BuyCartFailTestNotCustomer()
        {
            List<User> users = new List<User>();
            users.Add(_testUser);

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            var mockPurchase = new Mock<IPurchaseService>(MockBehavior.Loose);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            var result = controller.Buy(cartDto);
            var createdResult = result as ObjectResult;
            Assert.AreEqual(403, createdResult.StatusCode);
        }

        [TestMethod]
        public void BuyCartFailEmptyCart()
        {
            _testUser.AddRole(new Role() { Name = "Customer" });
            List<User> users = new List<User>();
            users.Add(_testUser);

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            var mockPurchase = new Mock<IPurchaseService>(MockBehavior.Loose);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            var result = controller.Buy(cartDto);
            var createdResult = result as BadRequestObjectResult;
            Assert.AreEqual("Empty Cart", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void BuyingMoreThanAvailableStock_Fails() 
        {
            User user = new User("prueba@gmail.com", "Calle 123", "password") { Id = 5, Token = "unbuentoken" };
            user.AddRole(new Role() { Name = "Customer" });
            List<User> users = new List<User>
            {
                user
            };

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            _mockProduct.Setup(s => s.Get(_testProduct.Id)).Returns(_testProduct);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 50
            };
            cartDto.PaymentMethod = "DEBIT";
            cartDto.Bank = "SANTANDER";
            cartDto.PaymentId = "ValidID";
            cartDto.Products.Add(cartLineDto);

            var result = controller.Buy(cartDto);
            var createdResult = result as BadRequestObjectResult;

            Assert.AreEqual("Not enough stock available to purchase " + _testProduct.Name, createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

      [TestMethod]
      public void BuyCartFailNoPaymentMethod()
      {
            User user = new User("prueba@gmail.com", "Calle 123", "password") { Id = 5, Token = "unbuentoken" };
            user.AddRole(new Role() { Name = "Customer" });
            List<User> users = new List<User>();
            users.Add(user);

            _mockUser.Setup(s => s.GetAll()).Returns(users);
            var mockPurchase = new Mock<IPurchaseService>(MockBehavior.Loose);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, mockPurchase.Object, _mockUser.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            CartDTO cartDto = new CartDTO();
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };
            cartDto.Products.Add(cartLineDto);
            var result = controller.Buy(cartDto);

            var createdResult = result as BadRequestObjectResult;
            Assert.AreEqual("Invalid payment method", createdResult.Value);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void CartDiscountAppliedWithExcludedProductsSuccess()
        {
            var controller = new CartController(_mockProduct.Object, _mockDiscounts.Object, _mockPurchase.Object, _mockUser.Object);


            CartDTO cartDto = new CartDTO();
            cartDto.PaymentMethod = "DEBIT";
            cartDto.Bank = "SANTANDER";
            CartLineDTO cartLineDto = new CartLineDTO()
            {
                Id = 1,
                Quantity = 3
            };

            cartDto.Products.Add(cartLineDto);

            cartLineDto = new CartLineDTO()
            {
                Id = 10,
                Quantity = 10
            };

            cartDto.Products.Add(cartLineDto);

            var result = controller.Create(cartDto);

            var createdResult = result as CreatedAtActionResult;

            Cart createdCart = (Cart)createdResult.Value;
            double expectedValue = 60600;
            Assert.AreEqual(expectedValue, createdCart.DiscountedPriceUYU);

        }
    }
}
