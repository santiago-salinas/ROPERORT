using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DataAccess.Entities
{
    public class AssignedRoles
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public string RoleName { get; set; }
        public RoleEntity Role { get; set; }

        public AssignedRoles() { }

        public static AssignedRoles FromModel(User user, Role role)
        {
            return new AssignedRoles
            {
                UserId = user.Id,
                User = UserEntity.FromModel(user),
                RoleName = role.Name,
                Role = RoleEntity.FromModel(role),
            };
        }
    }
}
