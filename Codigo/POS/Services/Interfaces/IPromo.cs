using Services.Models;
namespace Services.Interfaces
{
    public interface IPromo
    {
        string Name { get; }
        string Discount { get; }
        string Condition { get; }
        double ApplyDiscount(Cart cart);
    }

}
