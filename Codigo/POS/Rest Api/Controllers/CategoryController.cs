using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;

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
