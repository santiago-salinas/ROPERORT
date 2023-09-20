using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services;
using Services.Exceptions;
using Services.Interfaces;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<List<User>> GetAll() => _userService.GetAll();

    [HttpGet("{id}")]
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
            _userService.Add(user);
        }catch (Service_ObjectHandlingException e) 
        {
            return BadRequest(e.Message);
        }

        return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        User? user = _userService.Get(id);
        if (user == null)
            return NotFound();

        _userService.Delete(id);
        return NoContent();
    }
}