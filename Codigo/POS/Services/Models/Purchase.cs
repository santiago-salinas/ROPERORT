namespace Services.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Cart Cart { get; set; }
        public DateTime Date { get; set; }
    }
}