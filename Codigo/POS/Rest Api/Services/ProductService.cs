using System.Xml.Linq;
using Rest_Api.Models;

namespace Rest_Api.Services;


public class ProductService : IService<Product>
{
    List<Product> Products { get; }
    int nextId = 3;
    public ProductService()
    {
        Products = new List<Product>
        {
            new Product { Id = 1, Name = "Cap", PriceUYU = 600, Description="Stylish Cap.", Brand = "Nike", Category="Clothes", Colour="Beige" },
            new Product { Id = 2, Name = "Cap", PriceUYU = 600, Description="Stylish Cap.", Brand = "Nike", Category="Clothes", Colour="Beige" },
        };
    }

    public List<Product> GetAll() => Products;

    public Product? Get(int id) => Products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
    {
        product.Id = nextId++;
        Products.Add(product);
    }

    public void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
            return;

        Products.Remove(pizza);
    }

    public void Update(Product product)
    {
        var index = Products.FindIndex(p => p.Id == product.Id);
        if (index == -1)
            return;

        Products[index] = product;
    }
}