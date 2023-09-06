using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Entities
{
    public class ProductColors
    {
        public string ProductName { get; set; }
        public ProductEntity Product { get; set; }

        public string ColourName { get; set; }
        public ColourEntity Colour { get; set; }
    }
}
