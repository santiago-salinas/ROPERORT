using Rest_Api.Services;

namespace Rest_Api.Models.Promos
{
    public class TotalLookPromo : Promo
    {
        public TotalLookPromo() : base("Total look", "Having at least three products of the same color", "50% OFF most expensive product") { }

        public override double ApplyDiscount(Cart cart)
        {
            if (cart is null || cart.Products.Count == 0) { return 0; }

            double retValue = cart.PriceUYU;

            List<Colour> colorsInCart = new List<Colour>();

            foreach (CartLine cartLine in cart.Products)
            {
                if (cartLine.Product != null && cartLine.Product.Colours != null)
                {
                    foreach (Colour color in cartLine.Product.Colours)
                    {
                        if (!colorsInCart.Contains(color))
                        {
                            colorsInCart.Add(color);
                        }
                    }
                }
            }

            List<double> LowerDiscount = new List<double>();
            foreach (Colour colour in colorsInCart)
            {
                List<CartLine> cartLinesWithColor = cart.Products
                .Where(cartLine =>
                    cartLine.Product != null &&
                    cartLine.Product.Colours != null &&
                    cartLine.Product.Colours.Contains(colour))
                .ToList();

                cartLinesWithColor.OrderBy(p => p.Product.PriceUYU);


                int amountOfParticipatingProducts = 0;
                List<Product> participatingProducts = new List<Product>();

                while (amountOfParticipatingProducts < 3 && cartLinesWithColor.Count > 0)
                {
                    participatingProducts.Add(cartLinesWithColor.First().Product);
                    amountOfParticipatingProducts += cartLinesWithColor.First().Quantity;
                    cartLinesWithColor.Remove(cartLinesWithColor.First());
                }

                if (amountOfParticipatingProducts >= 3)
                {
                    double discount = (participatingProducts.Last().PriceUYU / 2.0);

                    LowerDiscount.Add(discount);
                }
            }
            if (LowerDiscount.Count() > 0)
            {
                LowerDiscount.Sort();
                return retValue - LowerDiscount.First();
            }

            return retValue;
        }

    }


}
