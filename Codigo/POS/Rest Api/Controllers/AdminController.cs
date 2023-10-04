using Microsoft.AspNetCore.Mvc;
using Rest_Api.Filters;
using Services;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]
    [ServiceFilter(typeof(AuthenticationFilter))]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPurchaseService _purchaseService;

        public AdminController(IUserService userService, IPurchaseService purchaseService)
        {
            _userService = userService;
            _purchaseService = purchaseService;
        }

        [HttpGet("Users")]
        public ActionResult<List<User>> GetAllUsers() => _userService.GetAll();

        [HttpGet("Users/{id}")]
        public ActionResult<User> Get(int id)
        {
            User? user = _userService.Get(id);
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpPost("Users")]
        public IActionResult Create(User user)
        {
            try
            {
                user.GenerateToken();
                _userService.Add(user);
            }
            catch (Service_ObjectHandlingException e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPut("Users/{id}")]
        public IActionResult Update(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            User? existingUser = _userService.Get(id);
            if (existingUser == null)
                return NotFound();

            _userService.Update(user);

            return NoContent();
        }

        [HttpDelete("Users/{id}")]
        public IActionResult Delete(int id)
        {
            User? user = _userService.Get(id);
            if (user == null)
                return NotFound();

            _userService.Delete(id);
            return NoContent();
        }

        [HttpGet("Purchases")]
        public ActionResult<List<Purchase>> GetAllPurchases() => _purchaseService.GetAll();
    }
}
