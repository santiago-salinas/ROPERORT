using System.Drawing;
using System.Xml.Linq;
using Rest_Api.Models;

namespace Rest_Api.Services;

public class BrandService : IGetService<Brand>
{
    List<Brand> Brands { get; }
    public BrandService()
    {
        Brands = new List<Brand>
        {
            new Brand {Name = "Nike"},
            new Brand {Name = "Puma"},
            new Brand {Name = "CAT"},
        };
    }

    public List<Brand> GetAll() => Brands;

    public Brand? Get(string name) => Brands.FirstOrDefault(p => p.Name == name);
}