using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Rest_Api.Filters;
using Services;
using Services.Interfaces;
using Services.Models;

namespace ApiTests.Filter
{
    [TestClass]
    public class AuthenticationFilterTest
    {
        private UserService _userService;
        private Mock<ICRUDRepository<User>> _userRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepository = new Mock<ICRUDRepository<User>>();

            var userList = new List<User>();

            var id = 1;
            var expectedUser = new User()
            {
                Id = id,
                Email = "example@gmail.com",
                Address = "Cuareim 1541",
                Password = "pass",
                Token = "tokentolen"
            };

            userList.Add(expectedUser);
            _userRepository.Setup(repo => repo.GetAll()).Returns(userList);
        }

        [TestMethod]
        public void TestAuthFilterWithoutHeader()
        {
            AuthenticationFilter authFilter = new AuthenticationFilter(_userRepository.Object);

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
        public void TestAuthFilterWithValidHeader()
        {
            AuthenticationFilter authFilter = new AuthenticationFilter(_userRepository.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "tokentolen";
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
        public void TestAuthFilterWithInvalidHeader()
        {
            AuthenticationFilter authFilter = new AuthenticationFilter(_userRepository.Object);

            var modelState = new ModelStateDictionary();
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["auth"] = "nottokentolen";

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
