using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;
using Services.Interfaces;
using Services.Models;

namespace Rest_Api.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public ExceptionFilter()
        {        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 500,
                Content = "An exception was thrown with this message: " + context.Exception.Message
            };
        }

    }
}