namespace Services.Models

{
    public class BoughtProduct
    {
        public Product Product { get; set; }
        public int Quantity
        {
            get => _quantity;
            set
            {
                ValidateQuantity(value);
                _quantity = value;
            }
        }

        private int _quantity;

        private void ValidateQuantity(int quantity)
        {
            if (quantity < 1)
                throw new Exception("Quantity must be more than 0");
        }
    }
}