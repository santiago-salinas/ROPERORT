using Moq;
using Services;
using Services.Interfaces;
using Services.Models;
using Services.Exceptions;



namespace ApiTests.Services
{
    [TestClass]
    public class GetServicesTests
    {
        private Mock<IGetRepository<Brand>> _brandRepository;
        private Mock<IGetRepository<Colour>> _colourRepository;
        private Mock<IGetRepository<Category>> _categoryRepository;
        private BrandService _brandService;
        private CategoryService _categoryService;
        private ColourService _colourService;

        [TestInitialize]
        public void TestInitialize()
        {
            _brandRepository = new Mock<IGetRepository<Brand>>(MockBehavior.Strict);
            _categoryRepository = new Mock<IGetRepository<Category>>(MockBehavior.Strict);
            _colourRepository = new Mock<IGetRepository<Colour>>(MockBehavior.Strict);

            _brandService = new BrandService(_brandRepository.Object);
            _categoryService = new CategoryService(_categoryRepository.Object);
            _colourService = new ColourService(_colourRepository.Object);
        }

        [TestMethod]
        public void GetAllWorks_ReturnsAllBrands()
        {
            List<Brand> expectedBrands = new List<Brand>
            {
                new Brand { Name = "Brand1" },
                new Brand { Name = "Brand2" },
            };
            _brandRepository.Setup(s => s.GetAll()).Returns(expectedBrands);
            var result = _brandService.GetAll();
            var createdResult = result;
            Assert.AreEqual(expectedBrands.Count, createdResult.Count);
        }

        [TestMethod]
        public void GetAllWorks_ReturnsAllColours()
        {
            List<Colour> expectedColours = new List<Colour>
            {
                new Colour { Name = "Colour1" },
                new Colour { Name = "Colour2" },
            };
            _colourRepository.Setup(s => s.GetAll()).Returns(expectedColours);
            var result = _colourService.GetAll();
            var createdResult = result;
            Assert.AreEqual(expectedColours.Count, createdResult.Count);
        }

        [TestMethod]
        public void GetAllWorks_ReturnsAllCategories()
        {
            List<Category> expectedCategories = new List<Category>
            {
                new Category { Name = "Category1" },
                new Category { Name = "Category2" },
            };
            _categoryRepository.Setup(s => s.GetAll()).Returns(expectedCategories);
            var result = _categoryService.GetAll();
            var createdResult = result;
            Assert.AreEqual(expectedCategories.Count, createdResult.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void GetAllCategories_Fails_ServiceExceptionThrown()
        {
            _categoryRepository.Setup(s => s.GetAll()).Throws(new DatabaseException("Get all error"));
            var result = _categoryService.GetAll();                       
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void GetAllBrands_Fails_ServiceExceptionThrown()
        {
            _brandRepository.Setup(s => s.GetAll()).Throws(new DatabaseException("Get all error"));
            var result = _brandService.GetAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void GetAllColours_Fails_ServiceExceptionThrown()
        {
            _colourRepository.Setup(s => s.GetAll()).Throws(new DatabaseException("Get all error"));
            var result = _colourService.GetAll();
        }

    }
}
