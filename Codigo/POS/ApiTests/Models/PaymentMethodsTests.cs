using Services.Exceptions;
using Services.Models.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTests.Models
{
    [TestClass]
    public class PaymentMethodsTests
    {
        [TestInitialize]
        public void TestInit()
        {

        }

        [TestMethod]
        public void GivenValidCompaniesCreditCardWorksProperly()
        {
            CreditCard c1 = new CreditCard() 
                { Id = "123456789", Company = "Visa" };
            CreditCard c2 = new CreditCard() 
                { Id = "123456789", Company = "MasterCard" };
            Assert.AreEqual(c1.Company, "VISA");
            Assert.AreEqual(c2.Company, "MASTERCARD");
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ArgumentException), "Invalid company")]
        public void GivenInvalidCompanyCreditCardThrowsException()
        {
            CreditCard card = new CreditCard()
                { Id = "123456789", Company = "NonExistent" };
        }

        [TestMethod]
        public void GivenValidBanksDebitWorksProperly()
        {

        }

        [TestMethod]
        public void GivenInvalidBankDebitThrowsException()
        {

        }
    }
}
