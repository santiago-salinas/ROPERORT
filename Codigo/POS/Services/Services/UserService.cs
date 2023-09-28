using Services.Interfaces;
using Services.Models;
using Services.Exceptions;

namespace Services;

public class UserService : IUserService
{
    private readonly ICRUDRepository<User> _repository;
    public UserService(ICRUDRepository<User> repository)
    {
        _repository = repository;
    }

    public void Add(User entity)
    {
        try
        {
            _repository.Add(entity);
        }
        catch (DatabaseException ex) 
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public void Delete(int id)
    {
        try
        {
            _repository.Delete(id);
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public User? Get(int id)
    {
        try
        {
            return _repository.Get(id);
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public List<User> GetAll()
    {
        try
        {
            return _repository.GetAll();
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public void Update(User entity)
    {
        try
        {
            _repository.Update(entity);
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }
}