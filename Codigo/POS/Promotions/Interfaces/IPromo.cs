namespace Promotions.Interfaces
{
    public interface IPromo
    {
        string Name { get; }
        string Discount { get; }
        string Condition { get; }
        double ApplyDiscount(ICart cart);
    }

}
