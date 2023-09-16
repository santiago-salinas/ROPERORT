namespace Rest_Api.Models
{
    public class Colour
    {
        public string Name { get; set; }

        public Colour() { }
        public Colour(string name) { Name = name; }

        public override bool Equals(Object obj)
        {
            return Name.Equals(((Colour)obj).Name);
        }
    }
}