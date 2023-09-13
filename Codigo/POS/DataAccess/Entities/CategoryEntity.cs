using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rest_Api.Models;

namespace DataAccess.Entities
{
    public class CategoryEntity
    {
        [Key]
        public string Name { get; set; }

        public static CategoryEntity FromModel(Category brand)
        {
            return new CategoryEntity() { Name = brand.Name };
        }

        public static Category FromEntity(CategoryEntity entity)
        {
            return new Category() { Name = entity.Name };
        }
    }
}
