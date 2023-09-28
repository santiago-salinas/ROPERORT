using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Exceptions;
using Rest_Api.Controllers.Exceptions;
using Rest_Api.Filters;
using Services.Interfaces;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
[ExceptionFilter]

public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    public PurchaseController(IPurchaseService purchaseService)
    { 
        _purchaseService = purchaseService; 
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Purchase>> GetAll() => _purchaseService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Purchase> Get(int id)
    {
        Purchase? product = _purchaseService.Get(id);

        if (product == null)
            return NotFound();

        return product;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Purchase product)
    {       
        try
        {
            _purchaseService.Add(product);
        }catch (Service_ObjectHandlingException e) 
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }   


    [HttpGet("history")]
    public ActionResult<List<Purchase>> GetPurchaseHistory([FromQuery] string email)
    {        
        return _purchaseService.GetPurchaseHistory(email);
    }

}