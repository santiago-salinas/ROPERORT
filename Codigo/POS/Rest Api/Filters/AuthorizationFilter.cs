using System;
using System.Threading.Tasks;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services;
using Services.Interfaces;
using Services.Models;

namespace Rest_Api.Filters
{
    public class ExampleAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private AuthService auth;
        private readonly string msg;
        public ExampleAuthorizationFilter(string message, ICRUDRepository<User> repo)
        {
            msg = message;
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
                    Content = msg + "no esta logueado."
                };
                return;
            }
            if (!auth.IsLogged(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = msg + "no esta identificado correctamente."
                };
                return;
            }
        }
    }
}