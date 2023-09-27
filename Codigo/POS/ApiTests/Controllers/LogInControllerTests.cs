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
        public string expectedToken = "3token16secure";

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IUserService>(MockBehavior.Strict);
            userController = new UserController(mock.Object);
            logInController = new LogInController(mock.Object);

            var usersGetAll = new List<User>();
            usersGetAll.Add(new User(email, "prueba", password) { Id=3,Token = expectedToken });
            mock.Setup(s => s.GetAll()).Returns(usersGetAll);
        }

        [TestMethod]
        public void GetTokenWithValidCredentials()
        {
            CredentialsDTO credentials = new CredentialsDTO(email, password);

            CreatedAtActionResult? result = logInController.Create(credentials) as CreatedAtActionResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedToken, result.Value);
        }

        [TestMethod]
        public void NotGetTokenWithInvalidCredentials()
        {
            CredentialsDTO credentials = new CredentialsDTO(email, "not" + password);

            BadRequestObjectResult? result = logInController.Create(credentials) as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void EmptyCredentials()
        {
            CredentialsDTO credentials = new CredentialsDTO("", "");

            BadRequestObjectResult? result = logInController.Create(credentials) as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}
