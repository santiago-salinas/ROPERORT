using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests.Controllers
{
    [TestClass]
    public class LoginControllerTests
    {
        private Mock<IUserService> mock;
        private UserController userController;
        private LogInController logInController;


        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IUserService>(MockBehavior.Strict);
            userController = new UserController(mock.Object);
            logInController = new LogInController(mock.Object);

        }

        [TestMethod]
        public void GetTokenWithValidCredentials()
        {
            string email = "prueba@gmail.com";
            string password = "password";
            string expectedToken = "3token16secure";
            mock.Setup(s => s.Get(3)).Returns(new User(3, email, "prueba", password) {Token = expectedToken });

            CredentialsDTO credentials = new CredentialsDTO(email, password);

            var result = logInController.LogIn(credentials);
            var createdResult = result as ActionResult<List<User>>;

            string testToken = result.Token;
            Assert.AreEqual(expectedToken, testToken);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid Credentials")]
        public void NotGetTokenWithInvalidCredentials()
        {
            string email = "prueba@gmail.com";
            string password = "password";
            string expectedToken = "3token16secure";
            mock.Setup(s => s.Get(3)).Returns(new User(3, email, "prueba", password) { Token = expectedToken });

            CredentialsDTO credentials = new CredentialsDTO(email, "not"+password);

            var result = logInController.LogIn(credentials);
            var createdResult = result as ActionResult<List<User>>;

            string testToken = result.Token;
            Assert.AreEqual(expectedToken, testToken);
        }

    }
}
