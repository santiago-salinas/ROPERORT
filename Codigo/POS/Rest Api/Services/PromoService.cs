using Rest_Api.Models;
using Rest_Api.Models.Promos;

namespace Rest_Api.Services;

public class PromoService
{
    private const int _zero = 0;

    List<Promo> Promos { get; }
    public PromoService()
    {
        Promos = new List<Promo>
        {
            new FidelityPromo(),
            new ThreeForTwoPromo(),
            new TwentyPercentOff(),
            new TotalLookPromo()
        };
    }


    public List<Promo> GetAll() => Promos;

    public void ApplyPromo(Cart cart)
    {
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

}