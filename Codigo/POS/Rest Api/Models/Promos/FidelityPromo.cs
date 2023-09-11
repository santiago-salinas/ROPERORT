using Rest_Api.Services;

namespace Rest_Api.Models.Promos
{
    public class FidelityPromo : Promo
    {
        public FidelityPromo() : base("3X1 Fidelity", "Having 3 products of the same brand", "The two cheapest are free") 
        { 
        }

        public override double ApplyDiscount(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
