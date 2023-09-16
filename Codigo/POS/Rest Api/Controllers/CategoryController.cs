using Microsoft.AspNetCore.Mvc;
using Rest_Api.Interfaces;
using Models;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        public IGetService<Category> categoryService;

        public CategoryController()
        {
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Category>> GetAll() => categoryService.GetAll();
    }
}
