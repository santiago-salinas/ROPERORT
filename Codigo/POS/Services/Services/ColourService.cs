using Services.Exceptions;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class ColourService : IGetService<Colour>
{
    private readonly IGetRepository<Colour> _repository;
    public ColourService(IGetRepository<Colour> repository)
    {
        _repository = repository;
    }

    public List<Colour> GetAll()
    {
        try { return _repository.GetAll(); }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        };
    }
    public Colour? Get(string name)
    {
        try { return _repository.Get(name); }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }
}
