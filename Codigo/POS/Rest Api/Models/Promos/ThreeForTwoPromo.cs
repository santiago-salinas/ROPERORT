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
            foreach(CartLine line in lines)
            {
                var discountedPrice = cart.PriceUYU;
                var product = line.Product;
                var category = product.Category;
                List<CartLine> categoryProducts = GetProductsFromCategory(lines, category);
                if(categoryProducts.Count > 2)
                {
                    double discount = GetCheapestProductValue(lines);
                    discountedPrice -= discount;
                    if(discountedPrice < bestValue)
                        bestValue = discountedPrice;
                }
            }

            return bestValue;
        }

        private List<CartLine> GetProductsFromCategory(List<CartLine> lines, Category category)
        {
            List<CartLine> result = new List<CartLine>();
            foreach(CartLine line in lines)
            {
                var product = line.Product;
                var productCategory = product.Category;
                if(productCategory.Equals(category))
                    result.Add(line);
            }
            return result;
        }

        private double GetCheapestProductValue(List<CartLine> cartLines)
        {
            List<CartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            var cheapestProduct = sortedByPrice[0].Product;
            return cheapestProduct.PriceUYU;
        }
    }
}
