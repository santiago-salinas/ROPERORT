using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Entities
{
    public class ColourEntity
    {
        [Key]
        public string Name { get; set; }
    }
}
