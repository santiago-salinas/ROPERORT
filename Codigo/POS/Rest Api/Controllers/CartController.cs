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
    private readonly PromoService _promoService;


    public CartController(ICRUDService<Product> cartService)
    {
        _productService = cartService;
        _promoService = new PromoService();
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
            _promoService.ApplyPromo(cart);
            return CreatedAtAction(nameof(Create),cart.DiscountedPriceUYU, cart);
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
}