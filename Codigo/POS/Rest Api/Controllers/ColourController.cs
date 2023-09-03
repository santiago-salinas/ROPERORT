using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class ColourController : ControllerBase
{
    public ColourService colourService;

    public ColourController()
    {
        colourService = new ColourService();
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Colour>> GetAll() =>
    colourService.GetAll();
}