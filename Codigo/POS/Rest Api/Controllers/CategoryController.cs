using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public IGetService<Category> _categoryService;

        public CategoryController(IGetService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Category>> GetAll() => _categoryService.GetAll();
    }
}
