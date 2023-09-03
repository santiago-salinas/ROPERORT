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
        public BrandBrand Brand { get; set; }
        public Categoryategory Category { get; set; }
        public Colourolour Colour { get; set; }
    }
}
