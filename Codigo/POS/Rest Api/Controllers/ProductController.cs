using Microsoft.AspNetCore.Mvc;
using Rest_Api.Filters;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;
using Rest_Api.Filters;
using Services.Models.Exceptions;
using Services.Models.DTOs;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
[ExceptionFilter]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    { _productService = productService; }


    [HttpGet]
    public ActionResult<List<Product>> GetAll
        ([FromQuery] ProductFilterDTO filters)
    {
        if (!CheckFiltersAreNull(filters))
        {
            return _productService.GetFiltered(filters);
        }
        else
        {
            return _productService.GetAll();
        }
    }

    private bool CheckFiltersAreNull(ProductFilterDTO filters)
    {
        var properties = typeof(ProductFilterDTO).GetProperties();
        return properties.All(property => property.GetValue(filters) == null);
    }

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

}