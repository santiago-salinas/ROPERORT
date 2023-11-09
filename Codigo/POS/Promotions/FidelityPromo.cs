using Services.Interfaces;
namespace Promotions

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


        public double ApplyDiscount(ICart cart)
        {
            double bestValue = cart.PriceUYU;
            List<ICartLine> lines = cart.Products;
            List<IBrand> brands = lines.Select(p => p.Product.Brand).Distinct().ToList();
            foreach (IBrand brand in brands)
            {
                var discountedPrice = cart.PriceUYU;
                List<ICartLine> brandProducts = GetProductsFromBrand(lines, brand);
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

        private List<ICartLine> GetProductsFromBrand(List<ICartLine> lines, IBrand brand)
        {
            List<ICartLine> result = lines.Where(p => p.Product.Brand.Name.Equals(brand.Name)).ToList();
            return result;
        }

        private double GetTwoCheapestProductsValues(List<ICartLine> cartLines)
        {
            List<ICartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            const int cheapestIndex = 0;
            ICartLine cheapestProductLine = sortedByPrice[cheapestIndex];
            IProduct cheapestProduct = cheapestProductLine.Product;
            double discount = cheapestProduct.PriceUYU;
            if (cheapestProductLine.Quantity > 1)
            {
                discount *= 2;
            }
            else
            {
                const int secondCheapestIndex = 1;
                IProduct secondProduct = sortedByPrice[secondCheapestIndex].Product;
                discount = cheapestProduct.PriceUYU + secondProduct.PriceUYU;
            }
            return discount;
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
