using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.PaymentMethods
{
    public class Paypal : PaymentMethod
    {
        public override string GetType()
        {
            return "Paypal";
        }
    }
}
