using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Rest_Api.DTOs;
using Services.Interfaces;
using Services.Models;

namespace ApiTests.Controllers
{
    [TestClass]
    public class LoginControllerTests
    {
        private Mock<IUserService> mock;
        private UserController userController;
        private LogInController logInController;

        public string email = "prueba@gmail.com";
        public string password = "password";
        public TokenDTO expectedToken;

        [TestInitialize]
        public void TestInitialize()
        {
            expectedToken = new TokenDTO ($"token{email}secure");

            mock = new Mock<IUserService>(MockBehavior.Strict);
            userController = new UserController(mock.Object);
            logInController = new LogInController(mock.Object);

            var usersGetAll = new List<User>
            {
                new User(email, "prueba", password) { Id = 3, Token = $"token{email}secure" }
            };
            mock.Setup(s => s.GetAll()).Returns(usersGetAll);
        }

        [TestMethod]
        public void GetTokenWithValidCredentials()
        {
            CredentialsDTO credentials = new CredentialsDTO(email, password);

            ActionResult<TokenDTO> result = logInController.Create(credentials);
            var createdResult = result.Result as OkObjectResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(expectedToken, createdResult.Value);
        }

        [TestMethod]
        public void NotGetTokenWithInvalidCredentials()
        {
            CredentialsDTO credentials = new CredentialsDTO(email, "not" + password);

            var result = logInController.Create(credentials);
            var createdResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, createdResult.StatusCode);
        }

        [TestMethod]
        public void EmptyCredentials()
        {
            CredentialsDTO credentials = new CredentialsDTO("", "");

            var result = logInController.Create(credentials);
            var createdResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(400, createdResult.StatusCode);
        }
    }
}
