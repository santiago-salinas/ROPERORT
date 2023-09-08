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
        if (cartDto == null || cartDto.Products.Count == 0)
        {
            return BadRequest("Empty Cart");
        }


        Cart cart = new Cart();
        try
        {
            cart = CartDTOtoObject(cartDto);
            return CreatedAtAction(nameof(Create),cart.PriceUYU, cart);
        }
        catch (Exception e)
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
                throw new ArgumentException("Product id was not found");
            }
            newline.Quantity = line.Quantity;

            ret.Products.Add(newline);
        }

        return ret;
    }

    [HttpGet]
    public ActionResult<CartDTO> GetAll()
    {
        CartDTO cartDto = new CartDTO();
        CartLineDTO cartLineDto = new CartLineDTO()
        {
            id = 1,
            Quantity = 3
        };

        cartDto.Products.Add(cartLineDto);

        return cartDto;
    }
}