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
            List<CartLine> productsAllBrands = lines.GroupBy(p => p.Product.Brand).Select(b => b.First()).ToList();
            foreach(CartLine productOfOneBrand in productsAllBrands)
            {
                var discountedPrice = cart.PriceUYU;
                var product = productOfOneBrand.Product;
                List<CartLine> brandProducts = GetProductsFromBrand(lines, product.Brand);
                if (ThereAreAtLeast3Products(brandProducts))
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

            List<CartLine> result = lines.Where(p => p.Product.Brand == brand).ToList();
            return result;
        }

        private double GetTwoCheapestProductsValues(List<CartLine> cartLines)
        {
            const int cheapestIndex = 0;
            const int secondCheapestIndex = 1;
            List<CartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            var cheapestProduct = sortedByPrice[cheapestIndex].Product;
            var secondCheapestProduct = sortedByPrice[secondCheapestIndex].Product;
            var value = cheapestProduct.PriceUYU + secondCheapestProduct.PriceUYU;
            return value;
        }

        private bool ThereAreAtLeast3Products(List<CartLine> list)
        {
            return list.Count > 2;
        }
    }
}
