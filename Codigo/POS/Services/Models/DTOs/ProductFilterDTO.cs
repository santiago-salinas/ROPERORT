using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.DTOs
{
    public class ProductFilterDTO
    {
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public double? MinimumPrice { get; set; }
        public double? MaximumPrice { get; set; }
        public bool? ExcludedFromPromos { get; set; }
    }

}
