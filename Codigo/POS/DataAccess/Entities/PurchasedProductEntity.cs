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
        [Column(Order = 1)]
        public int PurchaseId { get; set; }

        [Key]
        [ForeignKey(nameof(Product))]
        [Column(Order = 2)]
        public string ProductName { get; set; }


        public PurchaseEntity Purchase { get; set; }
        public ProductEntity Product { get; set; }
        public int Amount { get; set; }
    }
}
