using Microsoft.AspNetCore.Mvc;
using Rest_Api.Filters;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;
using Rest_Api.Filters;
using Services.Models.Exceptions;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
[ExceptionFilter]
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
    [ServiceFilter(typeof(AuthenticationFilter))]
    [ServiceFilter(typeof(AuthorizationFilter))]
    public IActionResult Create(Product product)
    {
        try
        {
            _productService.Add(product);
        }
        catch (Service_ObjectHandlingException e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }

    // PUT action
    [HttpPut("{id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public IActionResult Update(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();
        try
        {
            Product? existingProduct = _productService.Get(id);
            if (existingProduct == null)
                return NotFound();

            _productService.Update(product);
        }
        catch (Service_ObjectHandlingException e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }

    // DELETE action
    [HttpDelete("{id}")]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public IActionResult Delete(int id)
    {
        Product? product = _productService.Get(id);
        if (product == null)
            return NotFound();

        _productService.Delete(id);
        return Ok();
    }

    [HttpGet("filtered")]
    public ActionResult<List<Product>> GetFiltered(
        [FromQuery] string? category = null, 
        [FromQuery] string? brand = null, 
        [FromQuery] string? name = null,
        [FromQuery] double? minimumPrice = null,
        [FromQuery] double? maximumPrice = null,
        [FromQuery] bool? excludedFromPromos = null)
    {
        Category? categoryFilter = category != null ? new Category(category) : null;
        Brand? brandFilter = brand != null ? new Brand(brand) : null;
        return _productService.GetFiltered(categoryFilter, brandFilter, name,minimumPrice,maximumPrice,excludedFromPromos);
    }

}