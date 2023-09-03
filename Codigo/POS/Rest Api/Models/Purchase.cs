namespace Rest_Api.Models
{
    public class Purchase
    {
        public int Number { get; set; }
        public User Client { get; set; }
        public List<BoughtProduct> Products { get; set; }
    }
}