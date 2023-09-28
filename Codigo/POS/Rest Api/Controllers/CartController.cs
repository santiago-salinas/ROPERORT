using Microsoft.AspNetCore.Mvc;
using Rest_Api.DTOs;
using Services.Interfaces;
using Services.Models;
using Services.Models.Promos;
using Services;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IPromoService _promoService;

    private const int _zero = 0;

    public CartController(IProductService cartService, IPromoService promoService)
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
            cart = ApplyPromoToCart(cart);
            return CreatedAtAction(nameof(Create), cart.DiscountedPriceUYU, cart);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }

    [NonAction]
    public Cart ApplyPromoToCart(Cart cart)
    {
        List<Promo> promos = _promoService.GetAll();

        PromoApplier promoApplier = new PromoApplier(promos);

        Cart result = promoApplier.Apply(cart);

        return result;
    }

    [NonAction]
    private Cart CartDTOtoObject(CartDTO cartDto)
    {
        Cart ret = new Cart();

        foreach (CartLineDTO line in cartDto.Products)
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