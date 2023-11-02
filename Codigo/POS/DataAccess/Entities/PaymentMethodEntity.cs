using Services.Exceptions;
using Services.Models;
using Services.Models.PaymentMethods;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace DataAccess.Entities
{
    public class PaymentMethodEntity
    {
        [Key]
        public string Id { get; set; }
        public string Type { get; set; }
        public string Bank { get; set; }
        public string Company { get; set; }


        public PaymentMethodEntity() { }
        public static PaymentMethodEntity FromModel(PaymentMethod model, EFContext context)
        {
            PaymentMethodEntity entity = new PaymentMethodEntity()
            {
                Id = model.Id,
                Type = model.GetType(),
                Bank = "",
                Company = ""
            };
            if (entity.Type.Equals("Debit"))
                entity.Bank = model.ToString();
            if (entity.Type.Equals("CreditCard"))
                entity.Company = model.ToString();
            return entity;
        }

        public static PaymentMethod FromEntity(PaymentMethodEntity entity)
        {
            switch(entity.Type)
            {
                case "Paganza":
                    return new Paganza() { Id=entity.Id, };
                    break;
                case "Paypal":
                    return new Paypal() { Id = entity.Id, };
                    break;
                case "Debit":
                    return new Debit() 
                    { 
                        Id = entity.Id, 
                        Bank = entity.Bank,
                    };
                    break;
                case "CreditCard":
                    return new CreditCard()
                    {
                        Id = entity.Id,
                        Company = entity.Company,
                    };
                    break;
                default:
                    throw new DatabaseException("Not supported payment method");
            }
        }
    }
}
