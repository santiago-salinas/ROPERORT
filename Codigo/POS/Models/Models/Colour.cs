namespace Models
{
    public class Colour
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