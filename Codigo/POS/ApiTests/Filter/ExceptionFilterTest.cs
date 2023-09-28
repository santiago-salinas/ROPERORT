using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Rest_Api.Controllers;
using Rest_Api.Filters;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace ApiTests.Filter;

[TestClass]
public class ExceptionFilterTests
{
    [TestMethod]
    public void OnException_ReturnsInternalServerError()
    {
        // Arrange
        var filter = new ExceptionFilter();

        // Configura un HttpContext simulado
        var httpContext = new DefaultHttpContext();

        // Configura un ActionContext válido con un nombre
        var routeData = new RouteData();
        var actionDescriptor = new ControllerActionDescriptor
        {
            ControllerName = "Brand",
            ActionName = "GetAll",
            ControllerTypeInfo = typeof(BrandController).GetTypeInfo()
        };
        var actionContext = new ActionContext(httpContext, routeData, actionDescriptor);

        // Configura un ExceptionContext con la excepción simulada
        var context = new ExceptionContext(actionContext, Array.Empty<IFilterMetadata>())
        {
            Exception = new Exception("Test Exception")
        };

        // Act
        filter.OnException(context);

        // Assert
        Assert.IsInstanceOfType(context.Result, typeof(ContentResult));
        var contentResult = (ContentResult)context.Result;
        Assert.AreEqual(500, contentResult.StatusCode);
        StringAssert.Contains(contentResult.Content, "An exception was thrown with this message:");
    }
}
