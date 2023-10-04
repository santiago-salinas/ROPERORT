using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Rest_Api.Controllers;
using Rest_Api.Filters;
using Microsoft.AspNetCore.Routing;
using System.Reflection;
using Services;
using DataAccess.Repositories;
using DataAccess;
using Moq;
using Services.Interfaces;
using Services.Models;
using Services.Models.Exceptions;

namespace ApiTests.Filter;

[TestClass]
public class ExceptionFilterTests
{
    [TestMethod]
    public void OnException_ReturnsInternalServerError()
    {
        var filter = new ExceptionFilter();

        var httpContext = new DefaultHttpContext();

        var routeData = new RouteData();
        var actionDescriptor = new ControllerActionDescriptor
        {
            ControllerName = "Brand",
            ActionName = "GetAll",
            ControllerTypeInfo = typeof(BrandController).GetTypeInfo()
        };
        var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

        var context = new ExceptionContext(actionContext, Array.Empty<IFilterMetadata>())
        {
            Exception = new Exception("Test Exception")
        };

        filter.OnException(context);

        Assert.IsInstanceOfType(context.Result, typeof(ContentResult));
        var contentResult = (ContentResult)context.Result;
        Assert.AreEqual(500, contentResult.StatusCode);
        StringAssert.Contains(contentResult.Content, "An exception was thrown with this message:");
    }

    [TestMethod]
    public void OnModelException_ReturnsBadRequest()
    {
        var filter = new ExceptionFilter();

        var httpContext = new DefaultHttpContext();

        var routeData = new RouteData();
        var actionDescriptor = new ControllerActionDescriptor
        {
            ControllerName = "Product",
            ActionName = "Create",
            ControllerTypeInfo = typeof(ProductController).GetTypeInfo()
        };
        var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

        var context = new ExceptionContext(actionContext, Array.Empty<IFilterMetadata>())
        {
            Exception = new Models_ArgumentException("Test Exception")
        };

        filter.OnException(context);

        Assert.IsInstanceOfType(context.Result, typeof(ContentResult));
        var contentResult = (ContentResult)context.Result;
        Assert.AreEqual(400, contentResult.StatusCode);
        StringAssert.Contains(contentResult.Content, "Body parameters are invalid: ");
    }
}
