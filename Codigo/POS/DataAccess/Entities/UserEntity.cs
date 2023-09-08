using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Entities
{
    public class UserEntity
    {
        [Key]
        public string Email { get; set; }
        public string Address { get; set; }
        public IList<AssignedRoles> Roles { get; set; }
    }
}
