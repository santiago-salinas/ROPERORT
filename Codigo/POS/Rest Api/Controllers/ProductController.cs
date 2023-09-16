using Microsoft.AspNetCore.Mvc;
using Models;
using Rest_Api.Services;
using Rest_Api.Services.Exceptions;
using Rest_Api.Controllers.Exceptions;
using System.Drawing;
using Rest_Api.Interfaces;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    { _productService = productService; }

    // GET all action
    [HttpGet]
    public ActionResult<List<Product>> GetAll() => _productService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        Product? product = _productService.Get(id);

        if (product == null)
            return NotFound();

        return product;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Product product)
    {       
        try
        {
            _productService.Add(product);
        }catch (Service_ObjectHandlingException e) 
        {
            return BadRequest(e.Message);
        }

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }   

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        Product? existingProduct = _productService.Get(id);
        if (existingProduct == null)
            return NotFound();

        _productService.Update(product);

        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Product? product = _productService.Get(id);
        if (product == null)
            return NotFound();

        _productService.Delete(id);
        return NoContent();
    }

    [HttpGet("filtered")]
    public ActionResult<List<Product>> GetFiltered([FromQuery] string? category = null, [FromQuery] string? brand = null, [FromQuery] string? name = null)
    {
        Category? categoryFilter = category != null ? new Category(category) : null;
        Brand? brandFilter = brand != null ? new Brand(brand) : null;
        return _productService.GetFiltered(categoryFilter, brandFilter, name);
    }

}