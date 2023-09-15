using Rest_Api.DTOs;
using Rest_Api.Models.Exceptions;
using Rest_Api.Services;

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


        public double DiscountedPriceUYU
        {
            get
            {
                Promo possiblePromo = AppliedPromo;
                if (possiblePromo is not null)
                {
                    return possiblePromo.ApplyDiscount(this);
                }

                return TotalPrice();
            }

        }

        public Promo? AppliedPromo { get; set; }
        
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
