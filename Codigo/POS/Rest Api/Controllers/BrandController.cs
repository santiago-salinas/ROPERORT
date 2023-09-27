using Microsoft.AspNetCore.Mvc;
using Rest_Api.Filters;
using Services.Interfaces;
using Services.Models;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IGetService<Brand> _brandService;

        public BrandController(IGetService<Brand> service)
        {
            _brandService = service;
        }

        // GET all action
        [HttpGet]
        [ServiceFilter(typeof(AuthenticationFilter))]
        public ActionResult<List<Brand>> GetAll() => _brandService.GetAll();
    }
}
