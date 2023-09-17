using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess.Entities
{
    public class ProductColors
    {
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }

        public string ColourName { get; set; }
        public ColourEntity Colour { get; set; }


        public ProductColors() { }
        public ProductColors (ProductEntity product, Colour colour, EFContext context)
        {
            ColourEntity colourEntity = context.ColourEntities.First(c =>c.Name == colour.Name);

            ProductId = product.Id;
            Product = product;
            ColourName = colour.Name;
            Colour = colourEntity;
        }

       /* public static ProductColors FromEntity(ProductEntity productEntity, ColourEntity colourEntity)
        {
            return new ProductColors
            {
                ProductId = productEntity.Id,
                Product = productEntity,
                ColourName = colourEntity.Name,
                Colour = colourEntity
            };
        }*/
    }
}
