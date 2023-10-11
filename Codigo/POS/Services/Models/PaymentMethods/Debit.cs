using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.PaymentMethods
{
    public class Debit : PaymentMethod
    {
        private string _bank;

        public string Bank
        {
            get => _bank;
            set
            {
                ValidateBank(value);
                _bank = value.ToUpper();
            }
        }

        private void ValidateBank(string bank)
        {
            string bankAllCaps = bank.ToUpper();
            bool validBank = bankAllCaps.Equals("BBVA")
                || bankAllCaps.Equals("ITAU")
                || bankAllCaps.Equals("SANTANDER");
            if (!validBank)
                throw new Service_ArgumentException("Invalid bank");
        }
    }
}
