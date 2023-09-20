namespace Services.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public User Client { get; set; }
        public Cart Products { get; set; }
        public DateTime Date { get; set; }
        public string? AppliedPromotion { get; set; }
    }
}