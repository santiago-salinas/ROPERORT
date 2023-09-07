using Microsoft.AspNetCore.Mvc;
using Rest_Api.Models;
using Rest_Api.Services;
using Rest_Api.Controllers.Exceptions;
using Rest_Api.DTOs;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ICRUDService<Product> _productService;

    public CartController(ICRUDService<Product> cartService)
    {
        _productService = cartService;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(CartDTO cartDto)
    {
        if (cartDto == null)
        {
            return BadRequest();
        }


        Cart cart = new Cart();
        try
        {
            cart = CartDTOtoObject(cartDto);
            double price = cart.PriceUYU;
            return CreatedAtRoute("GetCartPrice", price);
        }
        catch (Controller_ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        
    }

    private Cart CartDTOtoObject(CartDTO cartDto)
    {
        Cart ret = new Cart();

        foreach(CartLineDTO line in cartDto.Products)
        {
            CartLine newline = new CartLine();
            
            newline.Product = _productService.Get(line.id);
            if (newline.Product == null)
            {
                throw new ArgumentException();
            }
            newline.Quantity = line.Quantity;

            ret.Products.Add(newline);
        }

        return ret;
    }
}