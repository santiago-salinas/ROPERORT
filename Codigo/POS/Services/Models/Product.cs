using Services.Models.Exceptions;

namespace Services.Models
{
    public class Product
    {
        public Product()
        { }
        public int Stock
        {
            get { return _stock; }
            set
            {
                if (value < 0)
                {
                    throw new Models_ArgumentException("Product stock cannot be below zero");
                }
                _stock = value;
            }
        }
        private double _priceUYU;
        private int _stock;
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Exclude { get; set; }

        public double PriceUYU
        {
            get { return _priceUYU; }
            set
            {
                if (value <= 0)
                {
                    throw new Models_ArgumentException("Product price cannot be zero or less");
                }
                _priceUYU = value;
            }
        }
        public string Description { get; set; }
        
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public List<Colour> Colours { get; set; }
    }
}
