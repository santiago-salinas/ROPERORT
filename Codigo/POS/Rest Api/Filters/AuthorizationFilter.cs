using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Interfaces;
using Services.Models;
using Services.Services;

namespace Rest_Api.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private static AuthorizationService _service;
        public AuthorizationFilter(ICRUDRepository<User> userRepository) 
        { 
            _service = new AuthorizationService(userRepository);
        }

        public void OnAuthorization(AuthorizationFilterContext context) 
        {
            string header = context.HttpContext.Request.Headers["Authorization"];
            if(header is null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Must receive a token"
                };
                return;
            }
            bool isAdmin = _service.IsAdmin(header);
            if(!isAdmin) 
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "This action is exclusive to administrators"
                };
                return;
            }
        }
    }
}