using Microsoft.AspNetCore.Mvc;
using Rest_Api.DTOs;
using Services.Interfaces;
using Services.Models;
using Rest_Api.Filters;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]

    public class LogInController : ControllerBase
    {
        private IUserService _userService;

        public LogInController(IUserService userService)
        {
            _userService = userService;
        }

        // POST action
        [HttpPost]
        public ActionResult<TokenDTO> Create(CredentialsDTO credentials)
        {
            List<User> users = _userService.GetAll();
            User? found = users.FirstOrDefault(user => user.Email == credentials.Email && user.Password == credentials.Password);

            if (found is null)
            {
                return BadRequest("Invalid Credentials");
            }

            TokenDTO response = new TokenDTO(found.Token);

            return Ok(response);
        }
    }
}