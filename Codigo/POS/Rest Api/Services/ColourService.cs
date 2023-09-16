using System.Drawing;
using System.Xml.Linq;
using DataAccessInterfaces;
using Models;
using Rest_Api.Interfaces;

namespace Rest_Api.Services;

public class ColourService : IGetService<Colour>
{
    private readonly IGetRepository<Colour> _repository;
    public ColourService(IGetRepository<Colour> repository)
    {
        _repository = repository;
    }

    public List<Colour> GetAll() => _repository.GetAll();

    public Colour? Get(string name) => _repository.Get(name);
}