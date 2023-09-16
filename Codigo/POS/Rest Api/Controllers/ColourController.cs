using Microsoft.AspNetCore.Mvc;
using Rest_Api.Interfaces;
using Models;
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