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
    public class PurchaseEntity
    {
        [Key]
        public int Id { get; set; }
        public UserEntity User { get; set; }        
        public ICollection<PurchasedProductEntity> Items { get; set; }
        public string? AppliedPromotion { get; set; }
        public DateTime Date { get; set; }


        public PurchaseEntity() { }
        public static PurchaseEntity FromModel(Purchase model, EFContext context)
        {
            Cart cart = model.Products;

            return new PurchaseEntity
            {
                Id = model.Id,
                User = UserEntity.FromModel(model.Client),
                Date = model.Date,
                AppliedPromotion = model.AppliedPromotion,
                Items = cart.Products.Select(p => PurchasedProductEntity.FromModel(model,p,context)).ToList(),                
            };
        }

        public static Purchase FromEntity(PurchaseEntity entity)
        {
            Cart cart = new Cart()
            {
                Products = entity.Items.Select(p => {
                    CartLine line = new CartLine()
                    {
                        Product = ProductEntity.FromEntity(p.Product),
                        Quantity = p.Amount,
                    };
                    return line;
                }).ToList(),                
            };

            return new Purchase
            {
                Id = entity.Id,
                Client = UserEntity.FromEntity(entity.User),
                Date = entity.Date,
                AppliedPromotion = entity.AppliedPromotion,
                Products = cart,                
            };
        }

    }
}
