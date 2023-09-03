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
        private const string ValidAdress = "Cuareim 1451";
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
                Adress = ValidAdress,
                Role = someRole
            };
        }

        [TestMethod]
        public void CreateUserSuccessTest()
        {
            var user = new User()
            {
                Mail = ValidMail2,
                Adress = ValidAdress,
                Role = someRole
            };
            Assert.IsNotNull(user);
            Assert.AreEqual(ValidMail2, user.Mail);
            Assert.AreEqual(ValidAdress, user.Adress);
            Assert.AreEqual(someRole.Name, user.Role.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Mail adress is not valid")]
        public void ThrowsExceptionWhenMailDosentHaveAt()
        {
            someUser.Mail = MailWithoutAt;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Mail adress is not valid")]
        public void ThrowsExceptionWhenMailDosentHaveDomain()
        {
            someUser.Mail = MailWithoutDomain;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Mail adress is not valid")]
        public void ThrowsExceptionWhenMailIsBlank()
        {
            someUser.Mail = NullString;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Adress is not valid")]
        public void ThrowsExceptionWhenAdressIsBlank()
        {
            someUser.Adress = NullString;
        }
    }
}
