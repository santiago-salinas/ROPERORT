using Microsoft.AspNetCore.Mvc;
using Rest_Api.Filters;
using Services.Interfaces;
using Services.Models;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]

    public class BrandController : ControllerBase
    {
        private readonly IGetService<Brand> _brandService;

        public BrandController(IGetService<Brand> service)
        {
            _brandService = service;
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Brand>> GetAll() => _brandService.GetAll();
    }
}
