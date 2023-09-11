﻿using Rest_Api.Services;

namespace Rest_Api.Models.Promos
{
    public class ThreeForTwoPromo : Promo
    {
        public ThreeForTwoPromo() : base("3X2", "Having 3 products of the same category", "The cheapest is free") 
        { 
        }

        public override double ApplyDiscount(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
