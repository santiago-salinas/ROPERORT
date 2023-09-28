using Services.Exceptions;
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

    public List<Brand> GetAll()
    {
        try { return _repository.GetAll(); }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        };
    }
    public Brand? Get(string name)
    {
        try { return _repository.Get(name); }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

}