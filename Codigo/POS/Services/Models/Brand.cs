using Services.Interfaces;

namespace Services.Models
{
    public class Brand : IBrand
    {
        public Brand() { }
        public Brand(string name) { Name = name; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            Brand otherBrand = (Brand)obj;
            return Name == otherBrand.Name;
        }
    }
}