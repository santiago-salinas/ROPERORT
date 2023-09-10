using System.Xml.Linq;
using Rest_Api.Models;
using Rest_Api.Services.Exceptions;

namespace Rest_Api.Services;



public class ProductService : ICRUDService<Product>
{
    List<Product> Products { get; }
    int nextId = 3;

    public IGetService<Colour> ColourService { get; set; }
    public IGetService<Brand> BrandService { get; set; }
    public IGetService<Category> CategoryService { get; set; }
    public ProductService()
    {
        Brand brand = new Brand();
        brand.Name = "Adidas";

        Category category = new Category();
        category.Name = "Shorts";

        Colour colour = new Colour();
        colour.Name = "Red";

        List<Colour> colours = new List<Colour>
        {
                colour
        };


        Products = new List<Product>
        {
            new Product { Id = 1, Name = "Cap1", PriceUYU = 600, Description="Stylish Cap.", Brand = brand, Category=category, Colours=colours },
            new Product { Id = 2, Name = "Cap2", PriceUYU = 600, Description="Stylish Cap.", Brand = brand, Category=category, Colours=colours },
        };
    }

    public List<Product> GetAll() => Products;

    public Product? Get(int id) => Products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product)
    {
        CheckProductParametersAreValid(product);
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
        CheckProductParametersAreValid(product);

        var index = Products.FindIndex(p => p.Id == product.Id);
        if (index == -1)
            return;

        Products[index] = product;
    }

    private void CheckProductParametersAreValid(Product product)
    {
        if (!BrandExists(product.Brand)) { throw new Service_ArgumentException("Brand does not exist"); }
        if (!CategoryExists(product.Category)) { throw new Service_ArgumentException("Category does not exist"); }
        if (!ColoursExist(product.Colours)) { throw new Service_ArgumentException("One of the Colours does not exist"); }
    }
    private bool BrandExists(Brand brand) => BrandService.Get(brand.Name) != null;
    private bool CategoryExists(Category category) => CategoryService.Get(category.Name) != null;
    private bool ColoursExist(List<Colour> colours)
    {
        foreach (Colour colour in colours)
        {
            if (ColourService.Get(colour.Name) == null)
            {
                return false;
            }
        }
        return true;
    }
}