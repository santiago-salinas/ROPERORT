using Services.Models.Exceptions;
using Services.Models.PaymentMethods;

namespace Services.Models
{
    public class Cart
    {
        public PaymentMethod PaymentMethod { get; set; }

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

            if (Products != null)
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
                double price;
                Promo possiblePromo = AppliedPromo;
                if (possiblePromo is not null)
                {
                    price = possiblePromo.ApplyDiscount(this);
                }
                else
                {
                    price = TotalPrice();
                }
                if (PaymentMethod == null)
                    return price;
                else
                    return PaymentMethod.ApplyDiscount(price);
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
                    throw new Models_ArgumentException("Quantity cannot be less or equal than 0.");
                }
                _quantity = value;
            }
        }

    }
}
