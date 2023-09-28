using Microsoft.AspNetCore.Mvc;
using Rest_Api.Filters;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
[ExceptionFilter]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public ActionResult<User> Get(int id)
    {
        User? user = _userService.Get(id);

        if (user == null)
            return NotFound();

        return user;
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        try
        {
            Role customerRole = new Role("Customer");
            user.AddRole(customerRole);
            _userService.Add(user);
        }
        catch (Service_ObjectHandlingException e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(AuthenticationFilter))]
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
}