using Services.Interfaces;

namespace Services.Models
{
    public abstract class Promo : IPromo
    {
        public Promo(string name, string condition, string discount)
        {
            Name = name;
            Condition = condition;
            Discount = discount;
        }
        public string Name { get; set; }
        public string Condition { get; set; }
        public string Discount { get; set; }

        public abstract double ApplyDiscount(ICart cart);

    }
}