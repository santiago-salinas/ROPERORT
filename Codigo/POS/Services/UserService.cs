using System.Xml.Linq;
using DataAccessInterfaces;
using Rest_Api.Models;
using Rest_Api.Services.Exceptions;

namespace Services;

public class UserService
{
    private readonly ICRUDRepository<User> _repository;
    public UserService(ICRUDRepository<User> repository)
    {
       _repository = repository;
    }

    public void Add(User entity)
    {
        _repository.Add(entity);
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }

    public User? Get(int id)
    {
        return _repository.Get(id);
    }

    public List<User> GetAll()
    {
        return _repository.GetAll();
    }

    public void Update(User entity)
    {
        _repository.Update(entity);
    }
}