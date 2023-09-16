using System.Drawing;
using System.Xml.Linq;
using DataAccessInterfaces;
using Rest_Api.Models;

namespace Services;

public class CategoryService
{
    private readonly IGetRepository<Category> _repository;
    public CategoryService(IGetRepository<Category> repository)
    {
        _repository = repository;
    }

    public List<Category> GetAll() => _repository.GetAll();

    public Category? Get(string name) => _repository.Get(name);
}