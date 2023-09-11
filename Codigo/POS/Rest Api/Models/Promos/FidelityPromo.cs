using Rest_Api.Services;

namespace Rest_Api.Models.Promos
{
    public class FidelityPromo : Promo
    {
        public FidelityPromo() : base("3X1 Fidelity", "Having 3 products of the same brand", "The two cheapest are free") 
        { 
        }

        public override double ApplyDiscount(Cart cart)
        {
            double bestValue = cart.PriceUYU;
            List<CartLine> lines = cart.Products;
            foreach (CartLine line in lines)
            {
                var discountedPrice = cart.PriceUYU;
                var product = line.Product;
                var brand = product.Brand;
                List<CartLine> brandProducts = GetProductsFromBrand(lines, brand);
                if (brandProducts.Count > 2)
                {
                    double discount = GetTwoCheapestProductsValues(lines);
                    discountedPrice -= discount;
                    if (discountedPrice < bestValue)
                        bestValue = discountedPrice;
                }
            }
            return bestValue;
        }

        private List<CartLine> GetProductsFromBrand(List<CartLine> lines, Brand brand)
        {
            List<CartLine> result = new List<CartLine>();
            foreach (CartLine line in lines)
            {
                var product = line.Product;
                var productBrand = product.Brand;
                if (productBrand.Equals(brand))
                    result.Add(line);
            }
            return result;
        }

        private double GetTwoCheapestProductsValues(List<CartLine> cartLines)
        {
            List<CartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            var cheapestProduct = sortedByPrice[0].Product;
            var secondCheapestProduct = sortedByPrice[1].Product;
            var value = cheapestProduct.PriceUYU + secondCheapestProduct.PriceUYU;
            return value;
        }
    }
}
