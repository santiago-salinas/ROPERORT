﻿using Rest_Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class PurchasedProductEntity
    {
        [Key]
        [ForeignKey(nameof(Purchase))]
        public int PurchaseId { get; set; }

        [Key]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public PurchaseEntity Purchase { get; set; }
        public ProductEntity Product { get; set; }
        public int Amount { get; set; }

        public static PurchasedProductEntity FromModel(Purchase purchase, CartLine cartLine)
        {
            return new PurchasedProductEntity
            {
                Amount = cartLine.Quantity,
                Product = ProductEntity.FromModel(cartLine.Product),
                ProductId = cartLine.Product.Id,
                Purchase = PurchaseEntity.FromModel(purchase),
                PurchaseId = purchase.Id,                
            };
        }
    }
}
