using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;
using Services.Interfaces;
using Services.Models;

namespace Rest_Api.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {

        private static AuthService auth;
        public AuthorizationFilter(ICRUDRepository<User> repo)
        {
            auth = new AuthService(repo);
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["auth"];
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Auth token is null"
                };
                return;
            }
            if (!auth.IsLogged(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Auth token is invalid"
                };
                return;
            }
        }
    }
}