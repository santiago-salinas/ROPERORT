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
    public class ColourEntity
    {
        [Key]
        public string Name { get; set; }

        public IList<ProductColors> ProductColors { get; set; }


        public ColourEntity() { }
        public static ColourEntity FromModel(Colour brand)
        {
            return new ColourEntity() { Name = brand.Name };
        }

        public static Colour FromEntity(ColourEntity entity)
        {
            return new Colour() { Name = entity.Name };
        }
    }
}
