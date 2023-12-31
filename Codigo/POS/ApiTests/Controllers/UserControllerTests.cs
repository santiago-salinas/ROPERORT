﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Controllers;
using Rest_Api.DTOs;
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
        public void GivenValidAuthGetReturnsUser()
        {
            List<User> expectedOutcome = new List<User>();
            User user = new User("prueba@gmail.com", "Calle 123", "password") { Id = 5, Token = "unbuentoken" };
            expectedOutcome.Add(user);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            mock.Setup(s => s.GetAll()).Returns(expectedOutcome);
            var result = userController.Get();
            var createdResult = result as ActionResult<User>;
            Assert.AreEqual(user, createdResult.Value);
        }


        [TestMethod]
        public void GivenInvalidAuthGetReturnsNotFound()
        {
            List<User> expectedOutcome = new List<User>();
            User user = new User("prueba@gmail.com", "Calle 123", "password") { Id = 5, Token = "unmalisimotoken" };
            expectedOutcome.Add(user);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            mock.Setup(s => s.GetAll()).Returns(expectedOutcome);
            var result = userController.Get();
            Assert.AreEqual(result.Value, null);
        }

        [TestMethod]
        public void GivenValidUserItGetsCreated()
        {
            User user = new User("prueba@gmail.com", "prueba", "password") { Id = 3 };
            UserDTO dto = new UserDTO()
            {
                Email = user.Email,
                Address = user.Address,
                Password = user.Password,
            };
            mock.Setup(s => s.Add(user));
            var result = userController.Create(dto);
            var createdResult = result as OkResult;
            Assert.AreEqual(createdResult.StatusCode, 200);
        }

        [TestMethod]
        public void GivenInvalidUserCreateReturnsBadRequest()
        {
            User user = new User("prueba@gmail.com", "prueba", "password");
            UserDTO dto = new UserDTO()
            {
                Email = user.Email,
                Address = user.Address,
                Password = user.Password,
            };
            mock.Setup(s => s.Add(It.Is<User>(u => u.Equals(user)))).Throws(new Service_ObjectHandlingException(""));
            var result = userController.Create(dto);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GivenCorrectValuesUserGetsUpdated()
        {
            User user = new User("prueba@gmail.com", "prueba", "password") { Id = 3 };
            user.GenerateToken();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = user.Token;
            userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            
            UserDTO userDTO = new UserDTO()
            {
                Address = "prueba",
                Email = "prueba@gmail.com",
                Password = "password"
            };

            mock.Setup(s => s.GetAll()).Returns(new List<User>() { user });
            mock.Setup(s => s.Update(user));
            var result = userController.Update(userDTO);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GivenNonExistentToken_UpdateReturnsNotFound()
        {
            User user = new User("prueba@gmail.com", "Calle", "password") { Id = 3 };
            user.GenerateToken();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "OtroToken";
            userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            User? nullUser = null;
            UserDTO userDTO = new UserDTO()
            {
                Address = "Calle",
                Email = "prueba@gmail.com",
                Password = "password"
            };
            mock.Setup(s => s.GetAll()).Returns(new List<User>() { user });
            var result = userController.Update(userDTO);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GivenValidToken_Delete_ReturnsNoContentResult()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "unbuentoken";

            userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            List<User> user = new List<User>() { new User("prueba@gmail.com", "Calle", "password") { Id = 6, Token = "unbuentoken", } };
            mock.Setup(s => s.GetAll()).Returns(user);
            mock.Setup(s => s.Delete(6));

            var result = userController.Delete();
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public void GivenInvalidToken_Delete_ReturnsNotFound()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "malToken";

            userController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            List<User> user = new List<User>() { new User("prueba@gmail.com", "Calle", "password") { Id = 6, Token = "unbuentoken", } };
            mock.Setup(s => s.GetAll()).Returns(user);

            var result = userController.Delete();
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
