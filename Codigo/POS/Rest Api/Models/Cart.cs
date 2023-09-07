namespace Rest_Api.Models
{
    public class Cart
    {
        public List<CartLine>? Products { get; set; }


        public double PriceUYU
        {
            get { return TotalPrice(); }
        }

        private double TotalPrice()
        {
            double sum = 0;

            if(Products != null)
            {
                foreach (CartLine line in Products)
                {
                    sum += line.Product.PriceUYU * line.Quantity;
                }
            }
            

            return sum;
        }
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}