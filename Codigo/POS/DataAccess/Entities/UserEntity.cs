using Services.Models;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public IList<AssignedRoles> Roles { get; set; }

        public UserEntity() { }

        public static UserEntity FromModel(User user)
        {
            return new UserEntity
            {
                Email = user.Email,
                Address = user.Address,
                Roles = user.Roles.Select(r => AssignedRoles.FromModel(user, r)).ToList(),
                Password = user.Password,
                Token = user.Token
            };
        }

        public static User FromEntity(UserEntity entity)
        {
            User user = new User(entity.Email, entity.Address, entity.Password);
            user.Id = entity.Id;
            foreach (AssignedRoles role in entity.Roles)
            {
                user.Roles.Add(RoleEntity.FromEntity(role.Role));
            }

            return user;
        }
    }
}
