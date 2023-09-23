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
    public class UserControllerTests
    {
        private Mock<IUserService> mock;
        private UserController userController;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IUserService>(MockBehavior.Strict);
            userController = new UserController(mock.Object);
        }

        [TestMethod]
        public void GetAllWorksCorrectly()
        {
            var expectedOutcome = new List<User>();
            mock.Setup(s => s.GetAll()).Returns(expectedOutcome);
            var result = userController.GetAll();
            var createdResult = result as ActionResult<List<User>>;
            Assert.AreEqual(expectedOutcome.Count, createdResult.Value.Count);
        }

        [TestMethod]
        public void GivenValidIdGetReturnsUser()
        {
            var expectedOutcome = new User(5, "prueba@gmail.com", "Calle 123");
            mock.Setup(s => s.Get(5)).Returns(expectedOutcome);
            var result = userController.Get(5);
            var createdResult = result as ActionResult<User>;
            Assert.AreEqual(expectedOutcome, createdResult.Value);
        }

        [TestMethod]
        public void GivenInvalidIdGetReturnsNotFound()
        {
            User? nullUser = null;
            mock.Setup(s => s.Get(7)).Returns(nullUser);
            var result = userController.Get(7);
            Assert.AreEqual(result.Value, null);
        }

        [TestMethod]
        public void GivenValidUserItGetsCreated()
        {
            var user = new User(3, "prueba@gmail.com", "prueba");
            mock.Setup(s => s.Add(user));
            var result = userController.Create(user);
            var createdResult = result as OkResult;
            Assert.AreEqual(createdResult.StatusCode, 200);
        }

        [TestMethod]
        public void GivenInvalidUserCreateReturnsBadRequest()
        {
            var user = new User(3, "prueba@gmail.com", "prueba");
            mock.Setup(s => s.Add(user)).Throws(new Service_ObjectHandlingException(""));
            var result = userController.Create(user);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GivenCorrectValuesUserGetsUpdated()
        {
            var user = new User(3, "prueba@gmail.com", "prueba");
            mock.Setup(s => s.Get(3)).Returns(user);
            mock.Setup(s => s.Update(user));
            var result = userController.Update(3, user);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GivenDifferentValuesUpdateReturnsBadRequest()
        {
            var user = new User(6, "prueba@gmail.com", "Calle");
            var result = userController.Update(5, user);
            Assert.IsInstanceOfType(result,typeof(BadRequestResult));
        }

        [TestMethod]
        public void GivenNonExistentIdUpdateReturnsNotFound()
        {
            var user = new User(6, "prueba@gmail.com", "Calle");
            User? nullUser = null;
            mock.Setup(s => s.Get(6)).Returns(nullUser);
            var result = userController.Update(6, user);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GivenValidIdUserGetsDeleted()
        {
            mock.Setup(s => s.Get(3)).Returns(new User(3, "prueba@gmail.com", "prueba"));
            mock.Setup(s => s.Delete(3));
            var result = userController.Delete(3);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GivenNonExistentIdDeleteReturnsNotFound()
        {
            User? nullUser = null;
            mock.Setup(s => s.Get(6)).Returns(nullUser);
            var result = userController.Delete(6);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
