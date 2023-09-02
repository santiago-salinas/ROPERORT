using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    public ProductService productService;

    public ProductController()
    {
        productService = new ProductService();
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Product>> GetAll() =>
    productService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Product> Get(int id)
    {
        var product = productService.Get(id);

        if (product == null)
            return NotFound();

        return product;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Product product)
    {
        productService.Add(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        var existingProduct = productService.Get(id);
        if (existingProduct is null)
            return NotFound();

        productService.Update(product);

        return NoContent();
    }


    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = productService.Get(id);
        if (product is null)
            return NotFound();

        productService.Delete(id);
        return NoContent();
    }
}