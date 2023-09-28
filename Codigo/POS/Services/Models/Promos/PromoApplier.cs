﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Promos
{
    public class PromoApplier
    {
        private const int _zero = 0;
        private List<Promo>? _promos = null;

        public PromoApplier(List<Promo>? promos)
        {
            _promos = promos;
        }

        public Cart Apply(Cart cart)
        {
            if (cart is null || cart.Products.Count == _zero) { return new Cart(); }
            if(_promos is null) { return new Cart(); }

            double bestPrice = cart.PriceUYU;
            Promo? bestPromoToClient = null;

            foreach (Promo promo in _promos)
            {
                double newPrice = promo.ApplyDiscount(cart);
                if (newPrice < bestPrice)
                {
                    bestPromoToClient = promo;
                    bestPrice = newPrice;
                }
            }

            if (bestPromoToClient != null)
            {
                cart.AppliedPromo = bestPromoToClient;
            }

            return cart;
        }
    }
}