using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        public IGetService<Brand> brandService;

        public BrandController()
        {
            brandService = new BrandService();
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Brand>> GetAll() => brandService.GetAll();
    }
}
