using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;
using System.Drawing;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    public IGetService<Role> roleService;

    public RoleController()
    {
        roleService = new RoleService();
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Role>> GetAll() => roleService.GetAll();
}