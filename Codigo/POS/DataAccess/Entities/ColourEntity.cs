using Services.Models;
using System.ComponentModel.DataAnnotations;


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
