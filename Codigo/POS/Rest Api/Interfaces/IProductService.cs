using Models;

namespace Rest_Api.Interfaces
{
    public interface IProductService
    {
        public List<Product> GetAll();

        public Product? Get(int id);

        public void Add(Product product);

        public void Delete(int id);

        public void Update(Product product);

        public List<Product> GetFiltered(Category? category = null, Brand? brand = null, string? name = null);
    }
}