namespace Rest_Api.Models
{
    public abstract class Promo
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

        public abstract double ApplyDiscount(Cart cart);        

    }
}