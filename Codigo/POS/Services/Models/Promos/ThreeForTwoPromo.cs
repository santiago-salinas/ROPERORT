using Services.Interfaces;

namespace Services.Models
{
    public class ThreeForTwoPromo : IPromo
    {
        public string Name { get; set; }
        public string Condition { get; set; }
        public string Discount { get; set; }

        public ThreeForTwoPromo()
        {
            Name = "3X2";
            Condition = "Having 3 products of the same category";
            Discount = "The cheapest is free";
        }

        public double ApplyDiscount(Cart cart)
        {
            double bestValue = cart.PriceUYU;
            List<CartLine> lines = cart.Products;
            List<Category> categories = lines.Select(p => p.Product.Category).Distinct().ToList();

            foreach (Category category in categories)
            {
                var discountedPrice = cart.PriceUYU;
                List<CartLine> categoryProducts = GetProductsFromCategory(lines, category);
                if (ThereAreAtLeast3Products(categoryProducts))
                {
                    double discount = GetCheapestProductValue(categoryProducts);
                    discountedPrice -= discount;
                    if (discountedPrice < bestValue)
                        bestValue = discountedPrice;
                }
            }
            return bestValue;
        }

        private List<CartLine> GetProductsFromCategory(List<CartLine> lines, Category category)
        {
            List<CartLine> result = lines.Where(p => p.Product.Category.Name.Equals(category.Name)).ToList();
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
            int quantity = 0;
            foreach (CartLine line in list)
                quantity += line.Quantity;
            return quantity > 2;
        }

    }
}
