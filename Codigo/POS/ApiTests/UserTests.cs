using Rest_Api.Models;

namespace ApiTests
{
    [TestClass]
    public class UserTests
    {
        private const int ValidId = 5;
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
            someUser = new User(ValidId, ValidMail1, ValidAddress);
        }

        [TestMethod]
        public void CreateUserSuccessTest()
        {
            var user = new User(ValidId, ValidMail2, ValidAddress);
            Assert.IsNotNull(user);
            Assert.AreEqual(ValidMail2, user.Mail);
            Assert.AreEqual(ValidAddress, user.Address);
        }

        [TestMethod]
        public void AddRoleSuccessTest()
        {
            someUser.AddRole(someRole);
            Assert.IsTrue(someUser.Roles.Contains(someRole));
        }

        [TestMethod]
        public void DoesntAddSameRoleTwice()
        {
            someUser.AddRole(someRole);
            Role newRole = new Role()
            {
                Name = "Customer"
            };
            someUser.AddRole(newRole);
            var expectedNumberOfRoles = 1;
            Assert.AreEqual(someUser.Roles.Count, expectedNumberOfRoles);
        }

        [TestMethod]
        public void RemoveRoleSuccessTest()
        {
            someUser.AddRole(someRole);
            someUser.RevokeRole(someRole);
            Assert.IsFalse(someUser.Roles.Contains(someRole));
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
