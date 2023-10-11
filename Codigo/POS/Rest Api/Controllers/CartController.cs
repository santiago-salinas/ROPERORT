using Microsoft.AspNetCore.Mvc;
using Rest_Api.DTOs;
using Services.Interfaces;
using Services.Models;
using Services.Models.Promos;
using Services.Exceptions;
using Services;
using Rest_Api.Filters;
using Microsoft.EntityFrameworkCore.Migrations;
using Services.Models.Exceptions;

namespace Rest_Api.Controllers;


[ApiController]
[Route("[controller]")]
[ExceptionFilter]

public class CartController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IPromoService _promoService;
    private readonly IPurchaseService _purchaseService;
    private readonly IUserService _userService;
    private const int _zero = 0;

    public CartController(IProductService cartService, IPromoService promoService, IPurchaseService purchaseService, IUserService userService)
    {
        _productService = cartService;
        _promoService = promoService;
        _purchaseService = purchaseService;
        _userService = userService;
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

    // POST action
    [HttpPost("buy")]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public IActionResult Buy(CartDTO cartDto)
    {
        List<User> users = _userService.GetAll();
        string auth = HttpContext.Request.Headers["auth"];
        User? user = users.FirstOrDefault(u => u.Token.Equals(auth));

        if (user == null || !user.Roles.Contains(new Role() { Name="Customer" })) 
        {
            return StatusCode(403, "User is not customer");
        }

        if (cartDto == null || cartDto.Products.Count == 0)
        {
            return BadRequest("Empty Cart");
        }

        Cart cart = new Cart();
        try
        {
            cart = CartDTOtoObject(cartDto);
            cart = ApplyPromoToCart(cart);
        }
        catch (Models_ArgumentException e)
        {
            return BadRequest(e.Message);
        }

        Purchase purchase = new Purchase()
        {
            Cart = cart,
            User = user,
            Date = DateTime.Now,            
        };

        _purchaseService.Add(purchase);
        return Ok();
    }


    [NonAction]
    private Cart ApplyPromoToCart(Cart cart)
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
                throw new Models_ArgumentException("Product id was not found");
            }

            if (newline.Product.Stock < line.Quantity)
            {
                throw new Models_ArgumentException("Not enough stock available to purchase " + newline.Product.Name);
            }

            newline.Quantity = line.Quantity;

            ret.Products.Add(newline);
        }

        return ret;
    }
}