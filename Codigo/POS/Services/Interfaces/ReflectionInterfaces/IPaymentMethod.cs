namespace Services.Interfaces
{
    public interface IPaymentMethod
    {
        public string Id { get; set; }
        public double ApplyDiscount(double price);        
        public string GetType();
    }
}
