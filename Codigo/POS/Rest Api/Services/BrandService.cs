using System.Drawing;
using System.Xml.Linq;
using DataAccessInterfaces;
using Models;
using Rest_Api.Interfaces;

namespace Rest_Api.Services;

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