using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;
using Rest_Api.Services.Exceptions;
using Rest_Api.Controllers.Exceptions;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private ICRUDService<User> _userService;
    private IGetService<Role> _roleService;

    public UserController()
    {
        _userService = new UserService();
        _roleService = new RoleService();
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<User>> GetAll() => _userService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        User? user = _userService.Get(id);

        if (user == null)
            return NotFound();

        return user;
    }

    // POST action
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

        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }   

    // PUT action
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

    // DELETE action
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