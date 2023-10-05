using Moq;
using Rest_Api.Controllers;
using Services.Interfaces;
using Services.Models;


namespace ApiTests.Controllers
{
    [TestClass]
    public class GetControllerSTests
    {
        private Mock<IGetService<Brand>> _brandService;
        private Mock<IGetService<Colour>> _colourService;
        private Mock<IGetService<Category>> _categoryService;
        private BrandController _brandController;
        private CategoryController _categoryController;
        private ColourController _colourController;

        [TestInitialize]
        public void TestInitialize()
        {
            _brandService = new Mock<IGetService<Brand>>(MockBehavior.Strict);
            _categoryService = new Mock<IGetService<Category>>(MockBehavior.Strict);
            _colourService = new Mock<IGetService<Colour>>(MockBehavior.Strict);

            _brandController = new BrandController(_brandService.Object);
            _categoryController = new CategoryController(_categoryService.Object);
            _colourController = new ColourController(_colourService.Object);
        }

        [TestMethod]
        public void GetAllWorks_ReturnsAllBrands()
        {
            List<Brand> expectedBrands = new List<Brand>
            {
                new Brand { Name = "Brand1" },
                new Brand { Name = "Brand2" },
            };
            _brandService.Setup(s => s.GetAll()).Returns(expectedBrands);
            var result = _brandController.GetAll();
            var createdResult = result;
            Assert.AreEqual(expectedBrands.Count, createdResult.Value.Count);
        }

        [TestMethod]
        public void GetAllWorks_ReturnsAllColours()
        {
            List<Colour> expectedColours = new List<Colour>
            {
                new Colour { Name = "Colour1" },
                new Colour { Name = "Colour2" },
            };
            _colourService.Setup(s => s.GetAll()).Returns(expectedColours);
            var result = _colourController.GetAll();
            var createdResult = result;
            Assert.AreEqual(expectedColours.Count, createdResult.Value.Count);
        }

        [TestMethod]
        public void GetAllWorks_ReturnsAllCategories()
        {
            List<Category> expectedCategories = new List<Category>
            {
                new Category { Name = "Category1" },
                new Category { Name = "Category2" },
            };
            _categoryService.Setup(s => s.GetAll()).Returns(expectedCategories);
            var result = _categoryController.GetAll();
            var createdResult = result;
            Assert.AreEqual(expectedCategories.Count, createdResult.Value.Count);
        }

    }
}
