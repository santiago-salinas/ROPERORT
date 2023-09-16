using System.Drawing;
using System.Xml.Linq;
using DataAccessInterfaces;
using Rest_Api.Models;

namespace Services;

public class BrandService
{
    private readonly IGetRepository<Brand> _repository;
    public BrandService(IGetRepository<Brand> repository)
    {
        _repository = repository;
    }

    public List<Brand> GetAll() => _repository.GetAll();

    public Brand? Get(string name) => _repository.Get(name);
}