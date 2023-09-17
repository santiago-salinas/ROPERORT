using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rest_Api.Services.Exceptions;
using Models;
using System.Linq;
using Rest_Api.Interfaces;
using Rest_Api.Services;
using Moq;
using DataAccessInterfaces;
using DataAccess.DatabaseServices;

namespace ApiTests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private ProductService _productService;

        private Mock<ICRUDRepository<Product>> _repository;
        private Mock<IGetRepository<Colour>> _colourRepository;
        private Mock<IGetRepository<Category>> _categoryRepository;
        private Mock<IGetRepository<Brand>> _brandRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new Mock<ICRUDRepository<Product>>(MockBehavior.Strict);

            _productService = new ProductService(_repository.Object);

            _colourRepository = new Mock<IGetRepository<Colour>>(MockBehavior.Strict);
            _categoryRepository = new Mock<IGetRepository<Category>>(MockBehavior.Strict);
            _brandRepository = new Mock<IGetRepository<Brand>>(MockBehavior.Strict);

            _productService.BrandRepository = _brandRepository.Object;
            _productService.ColourRepository = _colourRepository.Object;
            _productService.CategoryRepository = _categoryRepository.Object;
        }

        [TestMethod]
        public void GetAll_ReturnsListOfProducts()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            List<Product> products = _productService.GetAll();

            Assert.IsNotNull(products);
            Assert.IsTrue(products.Count == _mockProducts.Count);
        }

        [TestMethod]
        public void Get_ReturnsProductById()
        {
            int productId = 1;
            _repository.Setup(r => r.Get(productId)).Returns(_mockProducts[0]);

            Product product = _productService.Get(productId);

            Assert.AreEqual(product, _mockProducts[0]);
        }

        [TestMethod]
        public void Add_ProductIsAddedToList()
        {
            SetUpMocks();

            Brand brand = new Brand();
            brand.Name = "Puma";

            Category category = new Category();
            category.Name = "Pants";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            Product newProduct = new Product
            {
                Id = 3,
                Name = "Socks",
                PriceUYU = 200,
                Description = "Comfortable socks.",
                Brand = brand,
                Category = category,
                Colours = colours
            };

            _repository.Setup(r => r.Add(newProduct));
            _repository.Setup(r => r.Get(newProduct.Id)).Returns(newProduct);

            _productService.Add(newProduct);
            var addedProduct = _productService.Get(newProduct.Id);

            Assert.AreEqual(newProduct, addedProduct);
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ArgumentException))]
        public void Add_ProductFails()
        {
            _brandRepository.Setup(r => r.Get("404NOTFOUND")).Returns(null as Brand);
            _colourRepository.Setup(r => r.Get("404NOTFOUND")).Returns(null as Colour);
            _categoryRepository.Setup(r => r.Get("404NOTFOUND")).Returns(null as Category);


            Brand brand = new Brand();
            brand.Name = "404NOTFOUND";

            Category category = new Category();
            category.Name = "404NOTFOUND";

            Colour colour = new Colour();
            colour.Name = "404NOTFOUND";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            var newProduct = new Product
            {
                Id = 3,
                Name = "Socks",
                PriceUYU = 200,
                Description = "Comfortable socks.",
                Brand = brand,
                Category = category,
                Colours = colours
            };

            _productService.Add(newProduct);
            var addedProduct = _productService.Get(newProduct.Id);
        }

        [TestMethod]
        public void Delete_ProductIsRemovedFromList()
        {
            int productIdToRemove = 1;
            _repository.Setup(r => r.Get(productIdToRemove)).Returns(_mockProducts[0]);

            Product existingProduct = _productService.Get(productIdToRemove);
            Assert.IsNotNull(existingProduct);

            _repository.Setup(r => r.Get(productIdToRemove)).Returns(null as Product);
            _repository.Setup(r => r.Delete(productIdToRemove));

            _productService.Delete(productIdToRemove);
            Product deletedProduct = _productService.Get(productIdToRemove);
            Assert.IsNull(deletedProduct);
        }

        [TestMethod]
        public void Update_ProductIsUpdated()
        {
            SetUpMocks();
            int productIdToUpdate = 1;


            Brand brand = new Brand();
            brand.Name = "Puma";

            Category category = new Category();
            category.Name = "Pants";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<Colour> colours = new List<Colour>
            {
                colour
            };

            Product updatedProduct = new Product
            {
                Id = 1,
                Name = "Updated Cap",
                PriceUYU = 800,
                Description = "Updated description.",
                Brand = brand,
                Category = category,
                Colours = colours
            };
            _repository.Setup(r => r.Update(updatedProduct));
            _repository.Setup(r => r.Get(productIdToUpdate)).Returns(updatedProduct);

            _productService.Update(updatedProduct);
            Product product = _productService.Get(productIdToUpdate);

            Assert.IsNotNull(product);
            Assert.AreEqual(updatedProduct.Name, product.Name);
            Assert.AreEqual(updatedProduct.PriceUYU, product.PriceUYU);
            Assert.AreEqual(updatedProduct.Description, product.Description);
        }

        [TestMethod]
        public void GetFiltered_ByBrand()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            Brand filter = new Brand("Puma");

            List<Product> products = _productService.GetFiltered(brand: filter);

            Assert.AreEqual(2, products.Count);
            Assert.IsTrue(products.All(p => p.Brand.Equals(filter)));
        }

        [TestMethod]
        public void GetFiltered_ByCategory()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            Category filter = new Category("T-Shirt");

            List<Product> products = _productService.GetFiltered(category: filter);

            Assert.AreEqual(3, products.Count);
            Assert.IsTrue(products.All(p => p.Category.Equals(filter)));
        }

        [TestMethod]
        public void GetAll_FilterByVariosParameters()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            Category category = new Category("T-Shirt");
            Brand brand = new Brand("Nike");
            string name = "Other name";

            List<Product> products = _productService.GetFiltered(category, brand, name);

            Assert.AreEqual(0, products.Count);

            products = _productService.GetFiltered(category, brand);

            Assert.AreEqual(3, products.Count);
            Assert.IsTrue(products.All(p => p.Category.Equals(category) && p.Brand.Equals(brand)));
        }

        private List<Product> _mockProducts = new List<Product>()
        {
            new Product
            {
                Name = "Name 1",
                Description = "Description 1",
                PriceUYU = 20.0,
                Brand = new Brand("Nike"),
                Category = new Category("T-Shirt"),
                Colours = new List<Colour>() { new Colour("Blue")}
            },
            new Product
            {
                Name = "Name 2",
                Description = "Description 2",
                PriceUYU = 50.0,
                Brand = new Brand("Puma"),
                Category = new Category("Pants"),
                Colours = new List<Colour>() { new Colour("Green"), new Colour("Red") }
            },
            new Product
            {
                Name = "Name 3",
                Description = "Description 3",
                PriceUYU = 20.0,
                Brand = new Brand("Nike"),
                Category = new Category("T-Shirt"),
                Colours = new List<Colour> { new Colour("Red") }
            },

            new Product
            {
                Name = "Name 4",
                Description = "Description 4",
                PriceUYU = 100.0,
                Brand = new Brand("Puma"),
                Category = new Category("Pants"),
                Colours = new List<Colour> { new Colour("Green"), new Colour("Red") }
            },

            new Product
            {
                Name = "Name 5",
                Description = "Description 5",
                PriceUYU = 60.0,
                Brand = new Brand("Nike"),
                Category = new Category("T-Shirt"),
                Colours = new List<Colour> { new Colour("Blue"), new Colour("Green") }
            }
        };

        private List<Brand> _mockBrands = new List<Brand>()
        {
            new Brand("Puma"),
            new Brand("Nike"),
        };
        private List<Colour> _mockColours = new List<Colour>()
        {
            new Colour("Red"),
            new Colour("Green"),
            new Colour("Blue"),
        };
        private List<Category> _mockCategories = new List<Category>()
        {
            new Category("T-Shirt"),
            new Category("Pants"),
        };

        private void SetUpMocks()
        {
            ResetMocks();

            _brandRepository.Setup(r => r.Get("Puma")).Returns(_mockBrands[0]);
            _brandRepository.Setup(r => r.Get("Nike")).Returns(_mockBrands[1]);

            _colourRepository.Setup(r => r.Get("Red")).Returns(_mockColours[0]);
            _colourRepository.Setup(r => r.Get("Green")).Returns(_mockColours[1]);
            _colourRepository.Setup(r => r.Get("Blue")).Returns(_mockColours[2]);

            _categoryRepository.Setup(r => r.Get("T-Shirt")).Returns(_mockCategories[0]);
            _categoryRepository.Setup(r => r.Get("Pants")).Returns(_mockCategories[1]);
        }

        private void ResetMocks()
        {
            _brandRepository.Reset();
            _categoryRepository.Reset();
            _colourRepository.Reset();
        }
    }
}
