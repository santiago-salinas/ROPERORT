namespace Rest_Api.Models
{
    public class Cart
    {
        public User Client { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}