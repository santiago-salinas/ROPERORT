using Services.Exceptions;
using Services.Models.PaymentMethods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debit d1 = new Debit()
                { Id = "123456789", Bank = "Santander" };
            Debit d2 = new Debit()
                { Id = "123456789", Bank = "Itau" }; 
            Debit d3 = new Debit()
                { Id = "123456789", Bank = "bbva" };
            Assert.AreEqual(d1.Bank, "SANTANDER");
            Assert.AreEqual(d2.Bank, "ITAU");
            Assert.AreEqual(d3.Bank, "BBVA");
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ArgumentException), "Invalid bank")]
        public void GivenInvalidBankDebitThrowsException()
        {
            Debit debit = new Debit()
                { Id = "123456789", Bank = "NonExistent" };
        }
    }
}
