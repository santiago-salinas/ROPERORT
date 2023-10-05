using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using Rest_Api.Filters;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
[ExceptionFilter]

public class ColourController : ControllerBase
{
    public IGetService<Colour> _colourService;

    public ColourController(IGetService<Colour> colourService)
    {
        _colourService = colourService;
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Colour>> GetAll() => _colourService.GetAll();
}