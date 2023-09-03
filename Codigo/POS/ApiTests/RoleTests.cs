using Rest_Api.Models;

namespace ApiTests
{
    [TestClass]
    public class RoleTests
    {
        private const string Customer = "Customer";
        private const string Admin = "Admin";
        private const string Both = "Both";
        private const string InvalidRole = "prueba";

        private Role someRole;

        [TestInitialize]
        public void TestInit()
        {
            someRole = new Role()
            {
                Name = Customer
            };
        }

        [TestMethod]
        public void CreateRoleSuccessTest()
        {
            var role = new Role()
            {
                Name = Admin
            };
            Assert.IsNotNull(role);
            Assert.AreEqual(Admin, role.Name);
        }

        [TestMethod]
        public void SetBothRoleSuccessTest()
        {
            someRole.Name = Both;
            Assert.AreEqual(Both, someRole.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid role")]
        public void ThrowsExceptionWhenBuying0OrLessProducts()
        {
            someRole.Name = InvalidRole;
        }
    }
}
