using Moq;
using Services;
using Services.Interfaces;
using Services.Models;

namespace ApiTests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<ICRUDRepository<User>> _userRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepository = new Mock<ICRUDRepository<User>>();
            _userService = new UserService(_userRepository.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsListOfUsers()
        {
            var userList = new List<User>
            {
                new User(1, "user1@gmail.com", "Address1", "password"),
                new User(2, "user2@gmail.com", "Address2", "password"),
            };
            _userRepository.Setup(repo => repo.GetAll()).Returns(userList);

            var users = _userService.GetAll();

            Assert.IsNotNull(users);
            Assert.AreEqual(userList.Count, users.Count);
        }

        [TestMethod]
        public void Get_ReturnsUserById()
        {
            var id = 1;
            var expectedUser = new User(id, "user1@gmail.com", "Address1", "password");
            _userRepository.Setup(repo => repo.Get(id)).Returns(expectedUser);

            var user = _userService.Get(id);

            Assert.IsNotNull(user);
            Assert.AreEqual(expectedUser.Id, user.Id);
        }

        [TestMethod]
        public void Add_UserIsAddedToList()
        {
            var newUser = new User(2, "prueba@gmail.com", "Cuareim 1451", "password");
            _userRepository.Setup(repo => repo.Add(newUser));
            _userRepository.Setup(repo => repo.Get(newUser.Id)).Returns(newUser);

            _userService.Add(newUser);
            var addedUser = _userService.Get(newUser.Id);

            Assert.IsNotNull(addedUser);
            Assert.AreEqual(newUser.Id, addedUser.Id);
        }

        [TestMethod]
        public void Delete_UserIsRemovedFromList()
        {
            var userIdToRemove = 1;
            _userRepository.Setup(repo => repo.Delete(userIdToRemove));

            _userService.Delete(userIdToRemove);
            var deletedUser = _userService.Get(userIdToRemove);

            Assert.IsNull(deletedUser);
        }

        [TestMethod]
        public void Update_UserIsUpdated()
        {
            var userIdToUpdate = 1;
            var updatedUser = new User(userIdToUpdate, "prueba@hotmail.com", "Calle 1234", "password");
            _userRepository.Setup(repo => repo.Update(updatedUser));
            _userRepository.Setup(repo => repo.Get(userIdToUpdate)).Returns(updatedUser);

            _userService.Update(updatedUser);
            var user = _userService.Get(userIdToUpdate);

            Assert.IsNotNull(user);
            Assert.AreEqual(updatedUser.Id, user.Id);
            Assert.AreEqual(updatedUser.Email, user.Email);
            Assert.AreEqual(updatedUser.Address, user.Address);
        }
    }
}
