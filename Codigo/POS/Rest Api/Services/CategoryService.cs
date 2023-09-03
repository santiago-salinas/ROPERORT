using System.Drawing;
using System.Xml.Linq;
using Rest_Api.Models;

namespace Rest_Api.Services;

public class CategoryService : IGetService<Category>
{
    List<Category> Categories { get; }
    public CategoryService()
    {
        Categories = new List<Category>
        {
            new Category {Name = "Red"},
            new Category {Name = "Green"},
            new Category {Name = "Blue"},
        };
    }

    public List<Category> GetAll() => Categories;

    public Category? Get(string name) => Categories.FirstOrDefault(p => p.Name == name);
}