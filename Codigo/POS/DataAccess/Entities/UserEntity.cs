using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.Models;

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

        public static UserEntity FromModel (User user)
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

        public static User FromEntity (UserEntity entity)
        {
            User user = new User(entity.Id, entity.Email, entity.Address, "");
            foreach(AssignedRoles role in entity.Roles)
            {
                user.Roles.Add(RoleEntity.FromEntity(role.Role));
            }

            return user;
        }
    }
}
