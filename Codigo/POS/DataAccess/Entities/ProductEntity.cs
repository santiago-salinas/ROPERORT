using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Entities
{
    public class ProductEntity
    {
        [Key]
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public BrandEntity Brand { get; set; }
        public CategoryEntity Category { get; set; }
        public ICollection<ColourEntity> Colours { get; set;}
    }
}
