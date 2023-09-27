using Services.Interfaces;
using Services.Models;

namespace Services;
public class CategoryService : IGetService<Category>
{
    private readonly IGetRepository<Category> _repository;
    public CategoryService(IGetRepository<Category> repository)
    {
        _repository = repository;
    }

    public List<Category> GetAll() => _repository.GetAll();

    public Category? Get(string name) => _repository.Get(name);
}