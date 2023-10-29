using Promotions.Interfaces;

namespace Promotions.Promos
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

        public double ApplyDiscount(ICart cart)
        {
            double bestValue = cart.PriceUYU;
            List<ICartLine> lines = cart.Products;
            List<ICategory> categories = lines.Select(p => p.Product.Category).Distinct().ToList();

            foreach (ICategory category in categories)
            {
                var discountedPrice = cart.PriceUYU;
                List<ICartLine> categoryProducts = GetProductsFromCategory(lines, category);
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

        private List<ICartLine> GetProductsFromCategory(List<ICartLine> lines, ICategory category)
        {
            List<ICartLine> result = lines.Where(p => p.Product.Category.Name.Equals(category.Name)).ToList();
            return result;
        }

        private double GetCheapestProductValue(List<ICartLine> cartLines)
        {
            const int cheapestIndex = 0;
            List<ICartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            var cheapestProduct = sortedByPrice[cheapestIndex].Product;
            return cheapestProduct.PriceUYU;
        }

        private bool ThereAreAtLeast3Products(List<ICartLine> list)
        {
            int quantity = 0;
            foreach (ICartLine line in list)
                quantity += line.Quantity;
            return quantity > 2;
        }

    }
}
