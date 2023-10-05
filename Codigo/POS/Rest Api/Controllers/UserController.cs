﻿using Microsoft.AspNetCore.Mvc;
using Rest_Api.DTOs;
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

    [HttpGet]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public ActionResult<User> Get()
    {
        List<User> users = _userService.GetAll();
        string auth = HttpContext.Request.Headers["auth"];
        User? user = users.FirstOrDefault(u => u.Token.Equals(auth));

        if (user == null)
            return NotFound();

        return user;
    }

    [HttpPost]
    public IActionResult Create(UserDTO userDTO)
    {
        try
        {
            User newUser = new User(userDTO.Email, userDTO.Address, userDTO.Password);
            Role customerRole = new Role("Customer");
            newUser.AddRole(customerRole);
            _userService.Add(newUser);
        }
        catch (Service_ObjectHandlingException e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }

    [HttpDelete]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public IActionResult Delete()
    {
        List<User> users = _userService.GetAll();
        string auth = HttpContext.Request.Headers["auth"];
        User? user = users.FirstOrDefault(u => u.Token.Equals(auth));

        if (user == null)
            return NotFound();

        if(user is not null)
            _userService.Delete(user.Id);
        
        return NoContent();
    }

    [HttpPut]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public IActionResult Update(UserDTO userDTO)
    {
        List<User> users = _userService.GetAll();

        string auth = HttpContext.Request.Headers["auth"];
        User? existingUser = users.FirstOrDefault(u => u.Token.Equals(auth));

        if (existingUser == null)
            return NotFound();

        existingUser.Address = userDTO.Address;
        existingUser.Password = userDTO.Password;
        existingUser.Email = userDTO.Email;

        _userService.Update(existingUser);

        return NoContent();
    }
}