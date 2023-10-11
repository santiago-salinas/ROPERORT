using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.PaymentMethods
{
    public class Paganza : PaymentMethod
    {
        public override double ApplyDiscount(double price)
        {
            return price * 0.9;
        }
    }
}
