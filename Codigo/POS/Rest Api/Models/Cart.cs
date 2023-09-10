using Rest_Api.DTOs;
using Rest_Api.Models.Exceptions;

namespace Rest_Api.Models
{
    public class Cart
    {
        public List<CartLine> Products { get; set; }

        public Cart()
        {
            Products = new List<CartLine>();
        }

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
        
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (value <= 0)
                {
                    throw new Models_ArgumentException("Quantity cannot be less than 0.");
                }
                _quantity = value;
            }
        }

    }
}
