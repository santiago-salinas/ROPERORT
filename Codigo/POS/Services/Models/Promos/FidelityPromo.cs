using Services.Interfaces;

namespace Services.Models
{
    public class FidelityPromo : IPromo
    {
        public FidelityPromo()
        {
            Name = "3X1 Fidelity";
            Condition = "Having 3 products of the same brand";
            Discount = "The two cheapest are free";
        }

        public string Name { get; set; }
        public string Condition {  get; set; }
        public string Discount { get; set; }


        public double ApplyDiscount(Cart cart)
        {
            double bestValue = cart.PriceUYU;
            List<CartLine> lines = cart.Products;
            List<Brand> brands = lines.Select(p => p.Product.Brand).Distinct().ToList();
            foreach (Brand brand in brands)
            {
                var discountedPrice = cart.PriceUYU;
                List<CartLine> brandProducts = GetProductsFromBrand(lines, brand);
                if (ThereAreAtLeast3Products(brandProducts))
                {
                    double discount = GetTwoCheapestProductsValues(brandProducts);
                    discountedPrice -= discount;
                    if (discountedPrice < bestValue)
                        bestValue = discountedPrice;
                }
            }
            return bestValue;
        }

        private List<CartLine> GetProductsFromBrand(List<CartLine> lines, Brand brand)
        {
            List<CartLine> result = lines.Where(p => p.Product.Brand.Name.Equals(brand.Name)).ToList();
            return result;
        }

        private double GetTwoCheapestProductsValues(List<CartLine> cartLines)
        {
            List<CartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            const int cheapestIndex = 0;
            CartLine cheapestProductLine = sortedByPrice[cheapestIndex];
            Product cheapestProduct = cheapestProductLine.Product;
            double discount = cheapestProduct.PriceUYU;
            if (cheapestProductLine.Quantity > 1)
            {
                discount *= 2;
            }
            else
            {
                const int secondCheapestIndex = 1;
                Product secondProduct = sortedByPrice[secondCheapestIndex].Product;
                discount = cheapestProduct.PriceUYU + secondProduct.PriceUYU;
            }
            return discount;
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
