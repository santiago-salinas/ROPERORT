using Services.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        public PurchasedProductEntity() { }
        public static PurchasedProductEntity FromModel(Purchase purchase, CartLine cartLine, EFContext context)
        {
            return new PurchasedProductEntity
            {
                Amount = cartLine.Quantity,
                ProductId = cartLine.Product.Id,
                PurchaseId = purchase.Id,
            };
        }
    }
}
