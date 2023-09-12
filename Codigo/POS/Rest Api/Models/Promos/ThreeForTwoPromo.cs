using Rest_Api.Services;

namespace Rest_Api.Models.Promos
{
    public class ThreeForTwoPromo : Promo
    {
        public ThreeForTwoPromo() : base("3X2", "Having 3 products of the same category", "The cheapest is free") 
        { 
        }

        public override double ApplyDiscount(Cart cart)
        {
            double bestValue = cart.PriceUYU;
            List<CartLine> lines = cart.Products;
            List<CartLine> productsAllCategories = lines.GroupBy(p => p.Product.Category).Select(b => b.First()).ToList();
            foreach (CartLine productOfOneCategory in productsAllCategories)
            {
                var discountedPrice = cart.PriceUYU;
                var product = productOfOneCategory.Product;
                List<CartLine> categoryProducts = GetProductsFromCategory(lines, product.Category);
                if (ThereAreAtLeast3Products(categoryProducts))
                {
                    double discount = GetCheapestProductValue(lines);
                    discountedPrice -= discount;
                    if (discountedPrice < bestValue)
                        bestValue = discountedPrice;
                }
            }
            return bestValue;
        }

        private List<CartLine> GetProductsFromCategory(List<CartLine> lines, Category category)
        {
            List<CartLine> result = lines.Where(p => p.Product.Category == category).ToList();
            return result;
        }

        private double GetCheapestProductValue(List<CartLine> cartLines)
        {
            const int cheapestIndex = 0;
            List<CartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            var cheapestProduct = sortedByPrice[cheapestIndex].Product;
            return cheapestProduct.PriceUYU;
        }

        private bool ThereAreAtLeast3Products(List<CartLine> list)
        {
            return list.Count > 2;
        }
    }
}
