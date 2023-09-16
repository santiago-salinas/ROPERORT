namespace Rest_Api.Models
{
    public class Product
    {

        public Product()
        {
            Id = -1;
            Name = "";
            Description = "";
            PriceUYU = -1;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double PriceUYU { get; set; }
        public string Description { get; set; }


        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public List<Colour> Colours { get; set; }
    }
}
