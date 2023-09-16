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
    public class BrandEntity
    {
        [Key]
        public string Name { get; set; }

        public BrandEntity() { }

        public static BrandEntity FromModel(Brand brand) 
        {
            return new BrandEntity() { Name = brand.Name };
        }

        public static Brand FromEntity(BrandEntity entity)
        {
            return new Brand() { Name = entity.Name };
        }
    }
}
