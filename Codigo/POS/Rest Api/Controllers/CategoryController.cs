using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public IGetService<Category> categoryService;

        public CategoryController()
        {
            categoryService = new CategoryService();
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Category>> GetAll() => categoryService.GetAll();
    }
}
