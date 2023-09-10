namespace Rest_Api.Models
{
    public abstract class Promo
    {
        public Promo(string name, string condition, string discount) 
        {
            Name = name;
            Condition = condition;
            Discount = discount;
        }
        public string Name { get; set; }
        public string Condition { get; set; }
        public string Discount { get; set; }

        public abstract double ApplyDiscount(Cart cart);        

        protected Product MostExpensiveProduct(List<CartLine> products)
        {
            Product retValue = products[0].Product;
            double highestPrice = retValue.PriceUYU;

            foreach (CartLine line in products)
            {
                if(line.Product.PriceUYU > highestPrice)
                {
                    retValue = line.Product;
                    highestPrice = line.Product.PriceUYU;
                }
            }

            return retValue;
        }

        protected CartLine CheapestProduct(List<CartLine> products)
        {
            CartLine retValue = products[0];
            double lowestPrice = retValue.Product.PriceUYU;

            foreach (CartLine line in products)
            {
                if (line.Product.PriceUYU < lowestPrice)
                {
                    retValue = line;
                    lowestPrice = line.Product.PriceUYU;
                }
            }

            return retValue;
        }
    }


   
}