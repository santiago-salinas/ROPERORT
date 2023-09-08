using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Entities
{
    public class AssignedRoles
    {
        public string UserEmail { get; set; }
        public UserEntity User { get; set; }

        public string RoleName { get; set; }
        public RoleEntity Role { get; set; }
    }
}
