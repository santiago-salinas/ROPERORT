﻿using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using Rest_Api.Filters;

namespace Rest_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExceptionFilter]

    public class CategoryController : ControllerBase
    {
        public IGetService<Category> _categoryService;

        public CategoryController(IGetService<Category> categoryService)
        {
            _categoryService = categoryService;
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Category>> GetAll() => _categoryService.GetAll();
    }
}
