namespace Rest_Api.Models.Promos
{
    public class TwentyPercentOff : Promo
    {
        public TwentyPercentOff() : base("20% OFF", "Having 2 any products", "20% OFF most expensive product") { }

        override public double ApplyDiscount(Cart cart)
        {
            double retValue = cart.PriceUYU;

            if (HasAtLeastTwoProducts(cart))
            {
                Product mostExpensiveProduct = MostExpensiveProduct(cart);
                double mostExpensiveProductPrice = mostExpensiveProduct.PriceUYU;                

                retValue -= (mostExpensiveProductPrice * 0.8);
            }

            return retValue;
        }

        private bool HasAtLeastTwoProducts(Cart cart)
        {
            List<CartLine> lines = cart.Products;

            int amountOfItems = 0;
            foreach (CartLine line in lines)
            {
                amountOfItems += line.Quantity;
                if (amountOfItems == 2) return true;
            }

            return false;
        }

    }
}
