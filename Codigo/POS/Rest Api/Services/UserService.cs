using System.Xml.Linq;
using Rest_Api.Models;
using Rest_Api.Services.Exceptions;

namespace Rest_Api.Services;



public class UserService : ICRUDService<User>
{
    List<User> Users { get; }
    int nextId = 3;
    public IGetService<Role> RoleService { get; set; }

    public UserService()
    {
        Role role = new Role()
        {
            Name = "Customer"
        };

        var user1 = new User(1, "prueba@gmail.com", "Cuareim 1451");
        user1.AddRole(role);

        var user2 = new User(2, "prueba@hotmail.com", "Calle 1234");

        Users = new List<User>
        {
            user1, user2
        };
    }

    public void Add(User entity)
    {
        entity.Id = nextId++;
        Users.Add(entity);
    }

    public void Delete(int id)
    {
        var user = Get(id);
        if (user is null)
            return;
        Users.Remove(user);
    }

    public User? Get(int id)
    {
        return Users.FirstOrDefault(x => x.Id == id);
    }

    public List<User> GetAll()
    {
        return Users;
    }

    public void Update(User entity)
    {
        var index = Users.FindIndex(p => p.Id == entity.Id);
        if (index == -1)
            return;
        Users[index] = entity;
    }
}