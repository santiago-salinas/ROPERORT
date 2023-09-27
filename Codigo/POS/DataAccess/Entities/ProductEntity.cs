using Services.Models;
using System.ComponentModel.DataAnnotations;


namespace DataAccess.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public BrandEntity Brand { get; set; }
        public CategoryEntity Category { get; set; }

        public IList<ProductColors> Colours { get; set; }


        public ProductEntity() { Colours = new List<ProductColors>(); }
        public static ProductEntity FromModel(Product product, EFContext context)
        {
            BrandEntity brand = context.BrandEntities.First(b => b.Name == product.Brand.Name);
            CategoryEntity category = context.CategoryEntities.First(b => b.Name == product.Category.Name);

            ProductEntity retValue = new ProductEntity
            {
                Name = product.Name,
                Price = product.PriceUYU,
                Description = product.Description,
                Brand = brand,
                Category = category,
            };

            retValue.Colours = product.Colours.Select(c => new ProductColors(retValue, c, context)).ToList();

            return retValue;
        }

        public static Product FromEntity(ProductEntity entity)
        {
            return new Product
            {
                Id = entity.Id,
                Name = entity.Name,
                PriceUYU = entity.Price,
                Description = entity.Description,
                Brand = BrandEntity.FromEntity(entity.Brand),
                Category = CategoryEntity.FromEntity(entity.Category),
                Colours = entity.Colours.Select(c => ColourEntity.FromEntity(c.Colour)).ToList(),
            };
        }
    }
}
