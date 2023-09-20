using Services.Interfaces;
using Services.Models;

namespace Services;

public class BrandService : IGetService<Brand>
{
    private readonly IGetRepository<Brand> _repository;
    public BrandService(IGetRepository<Brand> repository)
    {
        _repository = repository;
    }

    public List<Brand> GetAll() => _repository.GetAll();
    public Brand? Get(string name) => _repository.Get(name);

}