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

        public string ProductName { get; set; }
        public PurchaseEntity Purchase { get; set; }
        public int Amount { get; set; }
        public double ProductPrice { get; set; }


        public PurchasedProductEntity() { }
        public static PurchasedProductEntity FromModel(Purchase purchase, CartLine cartLine, EFContext context)
        {
            return new PurchasedProductEntity
            {
                Amount = cartLine.Quantity,
                ProductName = cartLine.Product.Name,
                PurchaseId = purchase.Id,
                ProductPrice = cartLine.Product.PriceUYU,
            };
        }
    }
}
