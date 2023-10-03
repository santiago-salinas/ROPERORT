using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Exceptions;
using Services.Models.Exceptions;

namespace Rest_Api.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public ExceptionFilter()
        {        }

        public void OnException(ExceptionContext context)
        {
            if(context.Exception.GetType() == typeof(Models_ArgumentException)) 
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = "Body parameters are invalid: " + context.Exception.Message
                };
            }
            else
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 500,
                    Content = "An exception was thrown with this message: " + context.Exception.Message
                };
            }            
        }
    }
}