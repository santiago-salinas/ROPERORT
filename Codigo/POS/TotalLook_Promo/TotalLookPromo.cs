using Services.Interfaces;
using Services.Models;

namespace TotalLook
{
    public class TotalLookPromo : IPromo
    {
        private const int _minimumApplicableSameColour = 3;
        private const int _zero = 0;
        private const double _discountPercentage = 0.5;

        public string Name { get; set; }
        public string Condition { get; set; }
        public string Discount {  get; set; }

        public TotalLookPromo()
        {
            Name = "Total look";
            Condition = "Having at least three products of the same color";
            Discount = "50% OFF most expensive product";
        }

        public double ApplyDiscount(Cart cart)
        {
            if (cart is null || cart.Products.Count == _zero) { return _zero; }

            double retValue = cart.PriceUYU;

            List<Colour> colorsInCart = ColorsInCart(cart);
            List<double> Discounts = PossibleDiscounts(cart, colorsInCart);

            if (Discounts.Count() > _zero)
            {
                Discounts.Sort();
                double minimumViableDiscount = Discounts.First();
                return retValue - minimumViableDiscount;
            }

            return retValue;
        }

        private static List<double> PossibleDiscounts(Cart cart, List<Colour> colorsInCart)
        {
            List<double> Discounts = new List<double>();
            foreach (Colour colour in colorsInCart)
            {
                List<CartLine> cartLinesWithColor = cart.Products
                .Where(cartLine =>
                    cartLine.Product != null &&
                    cartLine.Product.Colours != null &&
                    cartLine.Product.Colours.Contains(colour))
                .ToList();

                cartLinesWithColor = cartLinesWithColor.OrderBy(p => p.Product.PriceUYU).ToList();

                int amountOfParticipatingProducts = _zero;
                List<Product> participatingProducts = new List<Product>();

                while (amountOfParticipatingProducts < _minimumApplicableSameColour && cartLinesWithColor.Count > _zero)
                {
                    CartLine cartLine = cartLinesWithColor.First();
                    participatingProducts.Add(cartLine.Product);
                    amountOfParticipatingProducts += cartLine.Quantity;
                    cartLinesWithColor.Remove(cartLine);
                }

                if (amountOfParticipatingProducts >= _minimumApplicableSameColour)
                {
                    participatingProducts = participatingProducts.OrderBy(p => p.PriceUYU).ToList();
                    double discount = (participatingProducts.Last().PriceUYU * _discountPercentage);

                    Discounts.Add(discount);
                }
            }

            return Discounts;
        }

        private static List<Colour> ColorsInCart(Cart cart)
        {
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

            return colorsInCart;
        }
    }


}
