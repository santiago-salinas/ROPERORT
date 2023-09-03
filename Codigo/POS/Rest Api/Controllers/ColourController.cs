using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;
using System.Drawing;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class ColourController : ControllerBase
{
    public IGetService<Colour> colourService;

    public ColourController()
    {
        colourService = new ColourService();
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Colour>> GetAll() => colourService.GetAll();
}