﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;

namespace Services.Models
{
    public class PromoApplier
    {
        private const int _zero = 0;
        private List<IPromo>? _promos = null;

        public PromoApplier(List<IPromo>? promos)
        {
            _promos = promos;
        }

        public Cart Apply(Cart cart)
        {
            if (cart is null || cart.Products.Count == _zero) { return new Cart(); }
            if(_promos is null) { return new Cart(); }

            Cart promotionalCart = RemoveExcludedProducts(cart);

            double bestPrice = promotionalCart.PriceUYU;
            IPromo? bestPromoToClient = null;

            foreach (IPromo promo in _promos)
            {
                double newPrice = promo.ApplyDiscount(promotionalCart);
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

        public Cart RemoveExcludedProducts(Cart cart)
        {
            Cart newCart = new Cart();

            foreach (CartLine cartLine in cart.Products)
            {
                Product product = cartLine.Product;
                bool isExcluded = product.Exclude;

                if (!isExcluded)
                {
                    newCart.Products.Add(cartLine);
                }
            }

            return newCart;
        }

    }
}
