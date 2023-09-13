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
    public class ProductEntity
    {
        [Key] 
        public int Id { get; set; }
        
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public BrandEntity Brand { get; set; }
        public CategoryEntity Category { get; set; }

        public IList<ProductColors> Colours { get; set;}

        public static ProductEntity FromModel(Product product)
        {
            return new ProductEntity
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.PriceUYU, // Assuming PriceUYU is an integer in ProductEntity
                Description = product.Description,
                Brand = BrandEntity.FromModel(product.Brand),
                Category = CategoryEntity.FromModel(product.Category),
                Colours = product.Colours.Select(c => ProductColors.FromModel(product,c)).ToList()
            };
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
