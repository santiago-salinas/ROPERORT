using Rest_Api.Models;

namespace ApiTests
{
    [TestClass]
    public class UserTests
    {
        private const string ValidMail1 = "prueba@gmail.com";
        private const string ValidMail2 = "prueba@outlook.uy";
        private const string MailWithoutAt = "pruebagmail.com";
        private const string MailWithoutDomain = "prueba@.";
        private const string ValidAddress = "Cuareim 1451";
        private const string NullString = "";

        private Role someRole = new Role()
        {
            Name = "Customer"
        };
        private User someUser;

        [TestInitialize]
        public void TestInit()
        {
            someUser = new User()
            {
                Mail = ValidMail1,
                Address = ValidAddress,
                Role = someRole
            };
        }

        [TestMethod]
        public void CreateUserSuccessTest()
        {
            var user = new User()
            {
                Mail = ValidMail2,
                Address = ValidAddress,
                Role = someRole
            };
            Assert.IsNotNull(user);
            Assert.AreEqual(ValidMail2, user.Mail);
            Assert.AreEqual(ValidAddress, user.Address);
            Assert.AreEqual(someRole.Name, user.Role.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Mail Address is not valid")]
        public void ThrowsExceptionWhenMailDosentHaveAt()
        {
            someUser.Mail = MailWithoutAt;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Mail Address is not valid")]
        public void ThrowsExceptionWhenMailDosentHaveDomain()
        {
            someUser.Mail = MailWithoutDomain;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Mail Address is not valid")]
        public void ThrowsExceptionWhenMailIsBlank()
        {
            someUser.Mail = NullString;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Address is not valid")]
        public void ThrowsExceptionWhenAddressIsBlank()
        {
            someUser.Address = NullString;
        }
    }
}
