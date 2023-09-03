using System.Xml.Linq;
using Rest_Api.Models;

namespace Rest_Api.Services;


public class ProductService : ICRUDService<Product>
{
    List<Product> Products { get; }
    int nextId = 3;
    public ProductService()
    {
        Brand brand = new Brand();
        brand.Name = "Adidas";

        Category category = new Category();
        category.Name = "Shorts";

        Colour colour = new Colour();
        colour.Name = "Red";

        Products = new List<Product>
        {
            new Product { Id = 1, Name = "Cap1", PriceUYU = 600, Description="Stylish Cap.", Brand = brand, Category=category, Colour=colour },
            new Product { Id = 2, Name = "Cap2", PriceUYU = 600, Description="Stylish Cap.", Brand = brand, Category=category, Colour=colour },
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