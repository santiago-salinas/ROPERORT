using System.Drawing;
using System.Xml.Linq;
using Rest_Api.Models;

namespace Rest_Api.Services;

public class RoleService : IGetService<Role>
{
    List<Role> Roles { get; }
    public RoleService()
    {
        Roles = new List<Role>
        {
            new Role {Name = "Customer"},
            new Role {Name = "Admin"},
        };
    }

    public List<Role> GetAll() => Roles;

    public Role? Get(string name) => Roles.FirstOrDefault(p => p.Name == name);
}