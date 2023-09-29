using Moq;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;
using Services;

namespace ApiTests.Services
{
    [TestClass]
    public class AuthServiceTests
    {
        private Mock<ICRUDRepository<User>> _userRepository;
        private AuthService _authService;

        [TestInitialize]
        public void TestInitialize()
        {
            _userRepository = new Mock<ICRUDRepository<User>>(MockBehavior.Strict);
            _authService = new AuthService(_userRepository.Object);
        }

        [TestMethod]
        public void LogIn_ValidUser_ReturnsToken()
        {
            var user = new User
            {
                Email = "test@example.com",
                Password = "password123"
            };

            var users = new List<User>
            {
                new User
                {
                    Email = "test@example.com",
                    Password = "password123",
                    Token = "token123"
                }
            };

            _userRepository.Setup(repo => repo.GetAll()).Returns(users);

            var result = _authService.LogIn(user);

            Assert.AreEqual("token123", result);
        }

        [TestMethod]
        public void LogIn_InvalidUser_ReturnsEmptyString()
        {
            var user = new User
            {
                Email = "test@example.com",
                Password = "wrongpassword"
            };

            var users = new List<User>
            {
                new User
                {
                    Email = "test@example.com",
                    Password = "password123",
                    Token = "token123"
                }
            };

            _userRepository.Setup(repo => repo.GetAll()).Returns(users);

            var result = _authService.LogIn(user);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void LogIn_DatabaseException_ThrowsServiceException()
        {
            _userRepository.Setup(repo => repo.GetAll()).Throws(new DatabaseException("Database error"));

            _authService.LogIn(new User());
        }

        [TestMethod]
        public void IsLogged_ExistingToken_ReturnsTrue()
        {
            var token = "token123";

            var users = new List<User>
            {
                new User
                {
                    Token = "token123"
                }
            };

            _userRepository.Setup(repo => repo.GetAll()).Returns(users);

            var result = _authService.IsLogged(token);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLogged_NonExistingToken_ReturnsFalse()
        {
            var token = "nonexistenttoken";

            var users = new List<User>();

            _userRepository.Setup(repo => repo.GetAll()).Returns(users);

            var result = _authService.IsLogged(token);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]

        public void IsLogged_GetAllFails_ThrowsException()
        {
            var token = "token123";

            var users = new List<User>
            {
                new User
                {
                    Token = "token123"
                }
            };

            _userRepository.Setup(repo => repo.GetAll()).Throws(new DatabaseException("Get all fails"));

            var result = _authService.IsLogged(token);
        }
    }
}
