using Services.Models;
using System.ComponentModel.DataAnnotations;


namespace DataAccess.Entities
{
    public class RoleEntity
    {
        [Key]
        public string Name { get; set; }
        public IList<AssignedRoles> AssignedRoles { get; set; }


        public RoleEntity() { }
        public static RoleEntity FromModel(Role role)
        {
            return new RoleEntity { Name = role.Name };
        }

        public static Role FromEntity(RoleEntity entity)
        {
            return new Role { Name = entity.Name };
        }
    }
}
