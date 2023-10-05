using Services.Models;
using System.ComponentModel.DataAnnotations;

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
