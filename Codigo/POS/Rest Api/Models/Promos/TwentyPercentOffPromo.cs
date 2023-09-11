﻿namespace Rest_Api.Models.Promos
{
    public class TwentyPercentOff : Promo
    {
        public TwentyPercentOff() : base("20% OFF", "Having 2 any products", "20% OFF most expensive product") { }

        override public double ApplyDiscount(Cart cart)
        {
            double retValue = cart.PriceUYU;

            if (HasAtLeastTwoProducts(cart))
            {
                Product productToDiscount = SecondCheapestProduct(cart.Products);
                double priceToApplyDiscuount = productToDiscount.PriceUYU;

                retValue -= (priceToApplyDiscuount * 0.8);
            }

            return retValue;
        }

        private Product SecondCheapestProduct(List<CartLine> cartLines)
        {
            List<CartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            return sortedByPrice[1].Product;
        }

        private bool HasAtLeastTwoProducts(Cart cart)
        {
            List<CartLine> lines = cart.Products;

            int amountOfItems = 0;
            foreach (CartLine line in lines)
            {
                amountOfItems += line.Quantity;
                if (amountOfItems == 2) return true;
            }

            return false;
        }

    }
}