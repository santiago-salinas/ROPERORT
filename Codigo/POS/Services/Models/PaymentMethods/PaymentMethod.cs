using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.PaymentMethods
{
    public abstract class PaymentMethod : IPaymentMethod
    {
        public string Id { get; set; }

        public virtual double ApplyDiscount(double price)
        {
            return price;
        }

        public abstract string GetType();
    }
}
