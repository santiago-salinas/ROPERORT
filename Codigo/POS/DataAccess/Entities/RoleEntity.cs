﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rest_Api.Models;


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
