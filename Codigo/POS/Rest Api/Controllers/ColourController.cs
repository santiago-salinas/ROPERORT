using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using System.Drawing;


namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class ColourController : ControllerBase
{
    public IGetService<Colour> colourService;

    public ColourController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Colour>> GetAll() => colourService.GetAll();
}