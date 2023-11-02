namespace Promotions.Interfaces
{
    public interface IPaymentMethod
    {
        public string Id { get; set; }
        public double ApplyDiscount();        
        public string GetType();
    }
}
