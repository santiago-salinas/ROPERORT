using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rest_Api.Filters;
using Services.Interfaces;
using Services.Models;
using Services;
using Rest_Api.Controllers;
using Newtonsoft.Json.Linq;
using System.Net;

namespace ApiTests.Fitler
{
    [TestClass]
    public class AuthorizationFilterTest
    {
        private Mock<ICRUDRepository<User>> _userRepository;
        private User _customer = new User(1, "example@gmail.com", "Cuareim 1541")
        {
            Password = "pass",
            Token = "customer"
        };
        private User _admin = new User(2, "example@outlook.com", "Cuareim 1541")
        {
            Password = "pass",
            Token = "admin"
        };

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepository = new Mock<ICRUDRepository<User>>();
            var userList = new List<User>();

            _customer.AddRole(new Role() { Name = "Customer" });
            _admin.AddRole(new Role() { Name = "Admin" });

            userList.Add(_customer);
            userList.Add(_admin);
            _userRepository.Setup(repo => repo.GetAll()).Returns(userList);
        }
        
        [TestMethod]
        public void TestAuthFilterWithoutHeader()
        {
            AuthorizationFilter authFilter = new AuthorizationFilter(_userRepository.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(401, response.StatusCode);
        }

        [TestMethod]
        public void ValidAdminTokenFilterTest()
        {
            AuthorizationFilter authFilter = new AuthorizationFilter(_userRepository.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "admin";
            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.IsNull(response);
        }

        [TestMethod]
        public void CustomerTokenFilterTest()
        {
            AuthorizationFilter authFilter = new AuthorizationFilter(_userRepository.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "customer";

            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(403, response.StatusCode);
        }

        [TestMethod]
        public void NonExistentAdminTokenFilterTest()
        {
            AuthorizationFilter authFilter = new AuthorizationFilter(_userRepository.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "nonexistent";

            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext: httpContext,
                                  routeData: new Microsoft.AspNetCore.Routing.RouteData(),
                                  actionDescriptor: new ActionDescriptor(),
                                  modelState: modelState),
                new List<IFilterMetadata>());

            authFilter.OnAuthorization(context);

            ContentResult response = context.Result as ContentResult;

            Assert.AreEqual(403, response.StatusCode);
        }
    }
}