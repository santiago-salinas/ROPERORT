using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Exceptions;
using Rest_Api.Filters;
using Services.Interfaces;
using Rest_Api.DTOs;
using Services;

namespace Rest_Api.Controllers;

[ApiController]
[Route("[controller]")]
[ExceptionFilter]

public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    private readonly IUserService _userService;
    public PurchaseController(IPurchaseService purchaseService, IUserService userService)
    {
        _purchaseService = purchaseService;
        _userService = userService;
    }

    // GET by Id action
    [HttpGet("{id}")]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public ActionResult<Purchase> Get(int id)
    {
        List<User> users = _userService.GetAll();
        string auth = HttpContext.Request.Headers["auth"];
        User? user = users.FirstOrDefault(u => u.Token.Equals(auth));

        Purchase? purchase = _purchaseService.Get(id);

        if (purchase == null)
            return NotFound();

        if(purchase.User.Id == user.Id)
            return purchase;

        return StatusCode(403, "Invalid authentication token");
    }

    // POST action
    [HttpPost]
    public IActionResult Create(CartDTO product)
    {
        return BadRequest("Purchases must be made trough URI/cart/purchase with valid auth token");
    }


    [HttpGet("history")]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public ActionResult<List<Purchase>> GetPurchaseHistory()
    {
        List<User> users = _userService.GetAll();
        string auth = HttpContext.Request.Headers["auth"];
        User? user = users.FirstOrDefault(u => u.Token.Equals(auth));

        
        return _purchaseService.GetPurchaseHistoryFromUser(user.Id);
    }

}