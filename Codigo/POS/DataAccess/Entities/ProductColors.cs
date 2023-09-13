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
    public class ProductColors
    {
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }

        public string ColourName { get; set; }
        public ColourEntity Colour { get; set; }

        public static ProductColors FromModel(Product product, Colour colour)
        {
            return new ProductColors
            {
                ProductId = product.Id,
                Product = ProductEntity.FromModel(product),
                ColourName = colour.Name,
                Colour = ColourEntity.FromModel(colour)
            };
        }

        public static ProductColors FromEntity(ProductEntity productEntity, ColourEntity colourEntity)
        {
            return new ProductColors
            {
                ProductId = productEntity.Id,
                Product = productEntity,
                ColourName = colourEntity.Name,
                Colour = colourEntity
            };
        }
    }
}
