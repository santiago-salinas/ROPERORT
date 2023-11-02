namespace Promotions.Interfaces
{
    public interface IProduct
    {
        public int Stock { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Exclude { get; set; }
        public double PriceUYU { get; set; }
        public string Description { get; set; }
        public IBrand Brand { get; set; }
        public ICategory Category { get; set; }
        public List<IColour> Colours { get; set; }
    }

    public interface IBrand
    {
        public string Name { get; set; }
    }

    public interface ICategory
    {
        public string Name { get; set; }
    }

    public interface IColour
    {
        public string Name { get; set; }
    }

}
