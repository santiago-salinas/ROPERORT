namespace Services.Interfaces
{
    public interface ICart
    {
        IPaymentMethod PaymentMethod { get; set; }
        List<ICartLine> Products { get; set; }
        double PriceUYU { get; }
        double DiscountedPriceUYU { get; }
        IPromo? AppliedPromo { get; set; }
    }
    public interface ICartLine
    {
        public int Quantity { get; set; }
        public IProduct Product { get; set; }
    }
}
