using Services.Interfaces;

namespace Services.Models
{
    public class Colour : IColour
    {
        public string Name { get; set; }

        public Colour() { }
        public Colour(string name) { Name = name; }

        public override bool Equals(object? obj)
        {
            Colour otherColour = (Colour)obj;
            return Name == otherColour.Name;
        }
    }
}