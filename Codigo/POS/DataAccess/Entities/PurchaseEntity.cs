using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Entities
{
    public class PurchaseEntity
    {
        [Key]
        public int Id { get; set; }
        public UserEntity User { get; set; }        
        public ICollection<PurchasedProductEntity> Items { get; set; }
        public string AppliedPromotion { get; set; }
        public DateTime Date { get; set; }

    }
}
