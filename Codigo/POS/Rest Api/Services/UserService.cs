using System.Xml.Linq;
using Rest_Api.Models;
using Rest_Api.Services.Exceptions;

namespace Rest_Api.Services;



public class UserService : ICRUDService<User>
{
    List<User> Users { get; }

    public IGetService<Role> RoleService { get; set; }

    public void Add(User entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public User? Get(int id)
    {
        throw new NotImplementedException();
    }

    public List<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(User entity)
    {
        throw new NotImplementedException();
    }
}