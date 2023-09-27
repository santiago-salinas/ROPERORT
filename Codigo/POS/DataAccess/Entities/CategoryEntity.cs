using Services.Models;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class CategoryEntity
    {
        [Key]
        public string Name { get; set; }

        public CategoryEntity() { }

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
