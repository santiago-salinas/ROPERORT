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

            return retValue;
        }

        private bool HasThreeSameColoredProducts(List<CartLine> products)
        {
            int amountOfItems = 0;
            foreach (CartLine line in products)
            {
                amountOfItems += line.Quantity;
                if (amountOfItems == 2) return true;
            }

            return false;
        }

        private List<Colour> ColoursInCommon(Product productA, Product productB)
        {
            return productA.Colours.Intersect(productB.Colours).ToList();
        }

        private bool HasThreeColoursInCommon(List<CartLine> lines)
        {

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    var commonColors = lines[i].Product.Colours.Intersect(lines[j].Product.Colours);

                    if (commonColors.Any())
                    {
                        for (int k = j + 1; k < lines.Count; k++)
                        {
                            if (commonColors.Intersect(lines[k].Product.Colours).Any())
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }


}
