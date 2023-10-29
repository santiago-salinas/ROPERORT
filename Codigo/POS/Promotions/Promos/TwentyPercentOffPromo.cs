using Promotions.Interfaces;
namespace Promotions.Promos
{
    public class TwentyPercentOff : IPromo
    {
        public string Name { get; set; }
        public string Condition { get; set; }
        public string Discount { get; set; }

        public TwentyPercentOff() 
        {
            Name = "20% OFF";
            Condition = "Having 2 any products";
            Discount = "20% OFF most expensive product";
        }

        public double ApplyDiscount(ICart cart)
        {
            double retValue = cart.PriceUYU;

            if (HasAtLeastTwoProducts(cart))
            {
                IProduct productToDiscount = SecondCheapestProduct(cart.Products);
                double priceToApplyDiscuount = productToDiscount.PriceUYU;

                retValue -= (priceToApplyDiscuount * 0.2);
            }

            return retValue;
        }

        private IProduct SecondCheapestProduct(List<ICartLine> cartLines)
        {
            List<ICartLine> sortedByPrice = cartLines.OrderBy(p => p.Product.PriceUYU).ToList();
            if (sortedByPrice.Count == 1 || sortedByPrice[0].Quantity >= 2)
            {
                return sortedByPrice[0].Product;
            }
            else
            {
                return sortedByPrice[1].Product;
            }
        }

        private bool HasAtLeastTwoProducts(ICart cart)
        {
            List<ICartLine> lines = cart.Products;

            int amountOfItems = 0;
            foreach (ICartLine line in lines)
            {
                amountOfItems += line.Quantity;
                if (amountOfItems == 2) return true;
            }

            return false;
        }

    }
}
