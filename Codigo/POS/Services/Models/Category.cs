using Services.Interfaces;

namespace Services.Models

{
    public class Category
    {
        public Category() { }
        public Category(string name) { Name = name; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            Category otherCategory = (Category)obj;
            return Name == otherCategory.Name;
        }
    }
}