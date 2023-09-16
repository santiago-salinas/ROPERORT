using Models;

namespace ApiTests
{
    [TestClass]
    public class RoleTests
    {
        private const string Customer = "Customer";
        private const string Admin = "Admin";
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
        public void EqualsWorksCorrectly()
        {
            var equalRole = new Role()
            {
                Name = Customer
            };
            var differentRole = new Role()
            {
                Name = Admin
            };
            Assert.AreEqual(someRole, equalRole);
            Assert.AreNotEqual(differentRole, equalRole);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid role")]
        public void ThrowsExceptionWhenGivenInvalidRoleName()
        {
            someRole.Name = InvalidRole;
        }
    }
}
