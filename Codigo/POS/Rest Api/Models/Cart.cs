namespace Rest_Api.Models
{
    public class Cart
    {
        public User Client { get; set; }
        public List<BoughtProduct> Products { get; set; }
    }
}