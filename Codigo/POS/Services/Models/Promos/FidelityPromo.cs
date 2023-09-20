

namespace Services.Models
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

            List<CartLine> result = lines.Where(p => p.Product.Brand == brand).ToList();
            return result;
        }

        private double GetTwoCheapestProductsValues(List<CartLine> cartLines)
        {
            List<CartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            const int cheapestIndex = 0;
            var cheapestProductLine = sortedByPrice[cheapestIndex];
            var cheapestProduct = cheapestProductLine.Product;
            var discount = cheapestProduct.PriceUYU;
            if (cheapestProductLine.Quantity > 1)
            {
                discount *= 2;
            }
            else
            {
                const int secondCheapestIndex = 1;
                var secondProduct = sortedByPrice[secondCheapestIndex].Product;
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
