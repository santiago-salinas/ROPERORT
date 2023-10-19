using Services.Models;
using Services.Models.DTOs;


namespace Services.Interfaces
{
    public interface IProductService
    {
        public List<Product> GetAll();

        public Product? Get(int id);

        public void Add(Product product);

        public void Delete(int id);

        public void Update(Product product);

        public List<Product> GetFiltered(ProductFilterDTO fitlers);
    }
}