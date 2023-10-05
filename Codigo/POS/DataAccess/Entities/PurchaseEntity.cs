using Services.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


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
        public double FinalPrice { get; set; }
        public double MoneyDiscounted { get; set; }


        public PurchaseEntity() { }
        public static PurchaseEntity FromModel(Purchase model, EFContext context)
        {
            Cart cart = model.Cart;

            return new PurchaseEntity
            {
                Id = model.Id,
                User = context.UserEntities.First(u => u.Email == model.User.Email),
                Date = model.Date,
                Items = cart.Products.Select(p => PurchasedProductEntity.FromModel(model, p, context)).ToList(),
                AppliedPromotion = cart.AppliedPromo?.Name ?? "No promo applied",
                MoneyDiscounted = cart.PriceUYU - cart.DiscountedPriceUYU,
                FinalPrice = cart.DiscountedPriceUYU,
            };
        }

        public static Purchase FromEntity(PurchaseEntity entity)
        {

            Cart cart = new Cart()
            {
                Products = entity.Items.Select(p =>
                {
                    CartLine line = new CartLine()
                    {
                        Product = ProductEntity.FromEntity(p.Product),
                        Quantity = p.Amount,
                    };
                    return line;
                }).ToList(),
                AppliedPromo = GetPromo(entity.AppliedPromotion),
            };

            return new Purchase
            {
                Id = entity.Id,
                User = UserEntity.FromEntity(entity.User),
                Date = entity.Date,
                Cart = cart,
            };
        }

        private static Promo? GetPromo(string name)
        {

            List<Promo> promoList = new List<Promo>
            {
                new FidelityPromo(),
                new ThreeForTwoPromo(),
                new TwentyPercentOff(),
                new TotalLookPromo()
            };

            return promoList.FirstOrDefault(p => p.Name == name);
        }

    }
}
