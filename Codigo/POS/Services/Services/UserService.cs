using Services.Interfaces;
using Services.Models;
using Services.Exceptions;

namespace Services;

public class UserService : IUserService
{
    private readonly ICRUDRepository<User> _userRepository;
    private readonly IGetRepository<Role> _roleRepository;
    public UserService(ICRUDRepository<User> userRepo, IGetRepository<Role> roleRepo)
    {
        _userRepository = userRepo;
        _roleRepository = roleRepo;
    }

    public void Add(User entity)
    {
        try
        {            
            _userRepository.Add(entity);
            if (!UserRolesAreValid(entity))
            {
                throw new Service_ArgumentException("User to be added contains inexistent role/s");
            }
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
            _userRepository.Delete(id);
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
            return _userRepository.Get(id);
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
            return _userRepository.GetAll();
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
            if (!UserRolesAreValid(entity))
            {
                throw new Service_ArgumentException("User is trying to be updated with inexistent roles");
            }
            _userRepository.Update(entity);
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    private bool UserRolesAreValid(User user)
    {
        List<Role> availableRoles = _roleRepository.GetAll();
        List<Role> userRoles = user.Roles;
        foreach (Role role in userRoles)
        {
            if (!availableRoles.Contains(role))
            {
                return false;
            }
        }

        return true;
    }
}