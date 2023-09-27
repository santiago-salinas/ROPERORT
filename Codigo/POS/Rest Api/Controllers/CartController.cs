using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Rest_Api.Controllers.Exceptions;
using Rest_Api.DTOs;
using Services.Interfaces;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly ICRUDService<Product> _productService;
    private readonly IGetService<Promo> _promoService;

    private const int _zero = 0;

    public CartController(ICRUDService<Product> cartService, IGetService<Promo> promoService)
    {
        _productService = cartService;
        _promoService = promoService;
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
            ApplyPromo(cart);
            return CreatedAtAction(nameof(Create),cart.DiscountedPriceUYU, cart);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [NonAction]
    public void ApplyPromo(Cart cart)
    {
        List<Promo> Promos = _promoService.GetAll();

        if (cart is null || cart.Products.Count == _zero) { return; }

        double bestPrice = cart.PriceUYU;
        Promo bestPromoToClient = null;

        foreach (Promo promo in Promos)
        {
            double newPrice = promo.ApplyDiscount(cart);
            if (newPrice < bestPrice)
            {
                bestPromoToClient = promo;
                bestPrice = newPrice;
            }
        }

        cart.AppliedPromo = bestPromoToClient;
    }

    [NonAction]
    private Cart CartDTOtoObject(CartDTO cartDto)
    {
        Cart ret = new Cart();

        foreach(CartLineDTO line in cartDto.Products)
        {
            CartLine newline = new CartLine();
            
            newline.Product = _productService.Get(line.Id);
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