namespace Rest_Api.Models
{
    public class Colour
    {
        public string Name { get; set; }

        public override bool Equals(Object obj)
        {
            return this.Name.Equals(((Colour)obj).Name);
        }
    }
}