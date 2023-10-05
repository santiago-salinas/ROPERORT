using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Rest_Api.DTOs;
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
    public class AdminControllerTests
    {
        private Mock<IUserService> mockUsers;
        private Mock<IPurchaseService> mockPurchases;
        private AdminController adminController;

        [TestInitialize]
        public void TestInitialize()
        {
            mockUsers = new Mock<IUserService>(MockBehavior.Strict);
            mockPurchases = new Mock<IPurchaseService>(MockBehavior.Strict);
            adminController = new AdminController(mockUsers.Object, mockPurchases.Object);
        }

        [TestMethod]
        public void GetAllUsersWorksCorrectly()
        {
            var expectedOutcome = new List<User>();
            mockUsers.Setup(s => s.GetAll()).Returns(expectedOutcome);
            var result = adminController.GetAllUsers();
            var createdResult = result as ActionResult<List<User>>;
            Assert.AreEqual(expectedOutcome.Count, createdResult.Value.Count);
        }

        [TestMethod]
        public void GivenValidIdGetReturnsUser()
        {
            var expectedOutcome = new User("prueba@gmail.com", "Calle 123", "password") { Id = 5 };
            mockUsers.Setup(s => s.Get(5)).Returns(expectedOutcome);
            var result = adminController.Get(5);
            var createdResult = result as ActionResult<User>;
            Assert.AreEqual(expectedOutcome, createdResult.Value);
        }

        [TestMethod]
        public void GivenInvalidIdGetReturnsNotFound()
        {
            User? nullUser = null;
            mockUsers.Setup(s => s.Get(7)).Returns(nullUser);
            var result = adminController.Get(7);
            Assert.AreEqual(result.Value, null);
        }

        [TestMethod]
        public void GivenInvalidUserCreateReturnsBadRequest()
        {
            var user = new User("prueba@gmail.com", "prueba", "password");
            mockUsers.Setup(s => s.Add(user)).Throws(new Service_ObjectHandlingException(""));
            var result = adminController.Create(user);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GivenCorrectValuesUserGetsUpdated()
        {
            var user = new User("prueba@gmail.com", "prueba", "password") { Id = 3 };
            mockUsers.Setup(s => s.Add(user));
            var result = adminController.Create(user);
            var createdResult = result as OkResult;
            Assert.AreEqual(createdResult.StatusCode, 200);
        }

        [TestMethod]
        public void GivenDifferentValuesUpdateReturnsBadRequest()
        {
            var user = new User("prueba@gmail.com", "Calle", "password") { Id = 4 };
            var result = adminController.Update(5, user);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GivenNonExistentIdUpdateReturnsNotFound()
        {
            var user = new User("prueba@gmail.com", "Calle", "password") { Id = 6 };
            User? nullUser = null;
            mockUsers.Setup(s => s.Get(6)).Returns(nullUser);
            var result = adminController.Update(6, user);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GivenValidIdUserGetsDeleted()
        {
            mockUsers.Setup(s => s.Get(3)).Returns(new User("prueba@gmail.com", "prueba", "password") { Id = 3 });
            mockUsers.Setup(s => s.Delete(3));
            var result = adminController.Delete(3);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GivenNonExistentIdDeleteReturnsNotFound()
        {
            User? nullUser = null;
            mockUsers.Setup(s => s.Get(6)).Returns(nullUser);
            var result = adminController.Delete(6);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllPurchasesWorksCorrectly()
        {
            var expectedOutcome = new List<Purchase>();
            mockPurchases.Setup(s => s.GetAll()).Returns(expectedOutcome);
            var result = adminController.GetAllPurchases();
            var createdResult = result as ActionResult<List<Purchase>>;
            Assert.AreEqual(expectedOutcome.Count, createdResult.Value.Count);
        }
    }
}
