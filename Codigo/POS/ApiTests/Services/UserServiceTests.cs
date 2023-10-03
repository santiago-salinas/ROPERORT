using Moq;
using Services;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;

namespace ApiTests.Services
{
    [TestClass]
    public class UserServiceTests
    {
        private UserService _userService;
        private Mock<ICRUDRepository<User>> _userRepository;
        private Mock<IGetRepository<Role>> _roleRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepository = new Mock<ICRUDRepository<User>>(MockBehavior.Strict);
            _roleRepository = new Mock<IGetRepository<Role>>(MockBehavior.Strict);
            _userService = new UserService(_userRepository.Object, _roleRepository.Object);
        }

        [TestMethod]
        public void GetAll_ReturnsListOfUsers()
        {
            var userList = new List<User>
            {
                new User("user1@gmail.com", "Address1", "password"){Id=1},
                new User("user2@gmail.com", "Address2", "password") { Id = 2 },
            };
            _userRepository.Setup(repo => repo.GetAll()).Returns(userList);

            var users = _userService.GetAll();

            Assert.IsNotNull(users);
            Assert.AreEqual(userList.Count, users.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void GetAll_Fails_ThrowsException()
        {
            _userRepository.Setup(r => r.GetAll()).Throws(new DatabaseException("Get all fails"));
            List<User> users = _userService.GetAll();
        }

        [TestMethod]
        public void Get_ReturnsUserById()
        {
            var id = 1;
            var expectedUser = new User("user1@gmail.com", "Address1", "password") { Id=id};
            _userRepository.Setup(repo => repo.Get(id)).Returns(expectedUser);

            var user = _userService.Get(id);

            Assert.IsNotNull(user);
            Assert.AreEqual(expectedUser.Id, user.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void Get_Fails_ThrowsException()
        {
            int userId = 1;
            _userRepository.Setup(r => r.Get(userId)).Throws(new DatabaseException("Get all fails"));
            User user = _userService.Get(userId);
        }

        [TestMethod]
        public void Add_UserIsAddedToList()
        {
            var newUser = new User("prueba@gmail.com", "Cuareim 1451", "password") { Id = 2 };
            _userRepository.Setup(repo => repo.Add(newUser));
            _userRepository.Setup(repo => repo.Get(newUser.Id)).Returns(newUser);

            _userService.Add(newUser);
            var addedUser = _userService.Get(newUser.Id);

            Assert.IsNotNull(addedUser);
            Assert.AreEqual(newUser.Id, addedUser.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void Add_Fails_ThrowsException()
        {
            var newUser = new User("prueba@gmail.com", "Cuareim 1451", "password") { Id = 2 };
            _userRepository.Setup(repo => repo.Add(newUser)).Throws(new DatabaseException("Add fails"));

            _userService.Add(newUser);
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
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void Delete_Fails_ThrowsException()
        {
            int userId = 1;
            _userRepository.Setup(r => r.Delete(userId)).Throws(new DatabaseException("Delete fails"));
            _userService.Delete(userId);
        }

        [TestMethod]
        public void Update_UserIsUpdated()
        {
            var userIdToUpdate = 1;
            var updatedUser = new User("prueba@hotmail.com", "Calle 1234", "password") { Id = userIdToUpdate };
            _userRepository.Setup(repo => repo.Update(updatedUser));
            _userRepository.Setup(repo => repo.Get(userIdToUpdate)).Returns(updatedUser);

            _userService.Update(updatedUser);
            var user = _userService.Get(userIdToUpdate);

            Assert.IsNotNull(user);
            Assert.AreEqual(updatedUser.Id, user.Id);
            Assert.AreEqual(updatedUser.Email, user.Email);
            Assert.AreEqual(updatedUser.Address, user.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]

        public void Update_Fails_ThrowsException()
        {
            var userIdToUpdate = 1;
            var updatedUser = new User("prueba@hotmail.com", "Calle 1234", "password") { Id = userIdToUpdate };
            _userRepository.Setup(repo => repo.Update(updatedUser)).Throws(new DatabaseException("Update fails"));

            _userService.Update(updatedUser);
        }
    }
}
