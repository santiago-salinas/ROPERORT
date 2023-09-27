using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;


namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
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