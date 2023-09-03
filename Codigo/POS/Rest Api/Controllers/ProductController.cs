using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;
using Rest_Api.Services.Exceptions;
using Rest_Api.Controllers.Exceptions;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private ICRUDService<Product> _productService;
    private IGetService<Colour> _colourService;
    private IGetService<Brand> _brandService;
    private IGetService<Category> _categoryService;

    public ProductController()
    {
        _productService = new ProductService();
        _colourService = new ColourService();
        _brandService = new BrandService();
        _categoryService = new CategoryService();
    }

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
            CheckProductParametersAreValid(product);
        }catch(Controller_ArgumentException e) 
        {
            return BadRequest(e.Message);
        }
        
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
        try
        {
            CheckProductParametersAreValid(product);
        }
        catch (Controller_ArgumentException e)
        {
            return BadRequest(e.Message);
        }

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

    private void CheckProductParametersAreValid(Product product)
    {
        if (!BrandExists(product.Brand)) { throw new Controller_ArgumentException("Brand does not exist"); }
        if (!CategoryExists(product.Category)) { throw new Controller_ArgumentException("Category does not exist"); }
        if (!ColourExists(product.Colour)) { throw new Controller_ArgumentException("Colour does not exist"); }
    }
    private bool BrandExists(Brand brand) => _brandService.Get(brand.Name) != null;
    private bool CategoryExists(Category category) => _categoryService.Get(category.Name) != null;
    private bool ColourExists(Colour colour) => _colourService.Get(colour.Name) != null;
}