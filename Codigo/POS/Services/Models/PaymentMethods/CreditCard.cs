using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.PaymentMethods
{
    public class CreditCard : PaymentMethod
    {
        private string _company;

        public string Company
        { 
            get => _company;
            set
            {
                ValidateCompany(value);
                _company = value.ToUpper();
            }
        }

        private void ValidateCompany(string company)
        {
            string companyAllCaps = company.ToUpper();
            bool validCompany = companyAllCaps.Equals("VISA")
                || companyAllCaps.Equals("MASTERCARD");
            if (!validCompany) 
                throw new Service_ArgumentException("Invalid company");
        }

        public override string GetType()
        {
            return "CreditCard";
        }
        public override string ToString()
        {
            return Company;
        }
    }
}
