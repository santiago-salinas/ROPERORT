using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_Api.Services.Exceptions;
using Rest_Api.Models;
using Rest_Api.Services;
using System.Linq;

namespace ApiTests
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService userService = new UserService();
        private IGetService<Role> _roleService = new RoleService();

        [TestInitialize] public void TestInitialize() 
        {
            userService.RoleService = _roleService;
        }

        [TestMethod]
        public void GetAll_ReturnsListOfUsers()
        {
            var users = userService.GetAll();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void Get_ReturnsUserById()
        {
            var id = 1;
            var user = userService.Get(id);

            Assert.IsNotNull(user);
            Assert.AreEqual(id, user.Mail);
        }

        [TestMethod]
        public void Add_UserIsAddedToList()
        {
            Role role = new Role()
            {
                Name = "Customer"
            };

            var newUser = new User(2, "prueba@gmail.com", "Cuareim 1451");
            newUser.AddRole(role);

            userService.Add(newUser);
            var addedUser = userService.Get(newUser.Id);

            Assert.IsNotNull(addedUser);
            Assert.AreEqual(newUser.Id, addedUser.Id);
        }

        [TestMethod]
        public void Delete_UserIsRemovedFromList()
        {
            var userIdToRemove = 1;
            userService.Delete(userIdToRemove);
            var deletedUser = userService.Get(userIdToRemove);
            Assert.IsNull(deletedUser);
        }

        [TestMethod]
        public void Update_UserIsUpdated()
        {
            var userIdToUpdate = 1;

            var updatedUser = new User(1, "prueba@hotmail.com", "Calle 1234");

            userService.Update(updatedUser);
            var user = userService.Get(userIdToUpdate);

            Assert.IsNotNull(user);
            Assert.AreEqual(updatedUser.Id, user.Id);
            Assert.AreEqual(updatedUser.Mail, user.Mail);
            Assert.AreEqual(updatedUser.Address, user.Address);
        }
    }
}
