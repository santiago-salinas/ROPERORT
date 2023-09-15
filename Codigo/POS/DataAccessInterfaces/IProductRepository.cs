
using Rest_Api.Models;
namespace DataAccessInterfaces
{
    public interface IProductRepository
    {
        abstract public List<Product> GetAll(Func<Product, bool>? filter);

        abstract public Product? Get(int id);

        abstract public void Add(Product entity);

        abstract public void Delete(int id);

        abstract public void Update(Product entity);
    }
}