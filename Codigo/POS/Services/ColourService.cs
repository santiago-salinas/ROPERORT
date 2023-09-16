using System.Drawing;
using System.Xml.Linq;
using DataAccessInterfaces;
using Rest_Api.Models;

namespace Services;

public class ColourService
{
    private readonly IGetRepository<Colour> _repository;
    public ColourService(IGetRepository<Colour> repository)
    {
        _repository = repository;
    }

    public List<Colour> GetAll() => _repository.GetAll();

    public Colour? Get(string name) => _repository.Get(name);
}