using Moq;
using Services;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;
using Services.Models.DTOs;
using System.Drawing;

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
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void GetAll_Fails_ThrowsException()
        {
            _repository.Setup(r => r.GetAll()).Throws(new DatabaseException("Get all fails"));
            List<Product> products = _productService.GetAll();
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
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void Get_Fails_ThrowsException()
        {
            int productId = 1;
            _repository.Setup(r => r.Get(productId)).Throws(new DatabaseException("Get all fails"));
           Product product = _productService.Get(productId);
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

            List<IColour> colours = new List<IColour>
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
        public void Add_InvalidBrand_Fails()
        {
            Brand brand = new Brand();
            brand.Name = "404NOTFOUND";

            Colour colour = new Colour();
            colour.Name = "Colour";

            Category category = new Category();
            category.Name = "Category";
            
            _brandRepository.Setup(r => r.Get(brand.Name)).Returns(null as Brand);
            _colourRepository.Setup(r => r.Get(colour.Name)).Returns(colour);
            _categoryRepository.Setup(r => r.Get(category.Name)).Returns(category);

            List<IColour> colours = new List<IColour>
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
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ArgumentException))]
        public void Add_InvalidColour_Fails()
        {
            Brand brand = new Brand();
            brand.Name = "Brand";
            
            Colour colour = new Colour();
            colour.Name = "Colour";

            Category category = new Category();
            category.Name = "Category";            

            _brandRepository.Setup(r => r.Get(brand.Name)).Returns(brand);
            _colourRepository.Setup(r => r.Get(colour.Name)).Returns(null as Colour);
            _categoryRepository.Setup(r => r.Get(category.Name)).Returns(category);

            List<IColour> colours = new List<IColour>
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
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ArgumentException))]
        public void Add_InvalidCategory_Fails()
        {
            Brand brand = new Brand();
            brand.Name = "Brand";

            Colour colour = new Colour();
            colour.Name = "Colour";

            Category category = new Category();
            category.Name = "404NOTFOUND";

            _brandRepository.Setup(r => r.Get(brand.Name)).Returns(brand);
            _colourRepository.Setup(r => r.Get(colour.Name)).Returns(colour);
            _categoryRepository.Setup(r => r.Get(category.Name)).Returns(null as Category);

            List<IColour> colours = new List<IColour>
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
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void Add_Fails_ThrowsExcpetion()
        {
            Brand brand = new Brand();
            brand.Name = "Brand";

            Colour colour = new Colour();
            colour.Name = "Colour";

            Category category = new Category();
            category.Name = "Category";

            _brandRepository.Setup(r => r.Get(brand.Name)).Returns(brand);
            _colourRepository.Setup(r => r.Get(colour.Name)).Returns(colour);
            _categoryRepository.Setup(r => r.Get(category.Name)).Returns(category);

            List<IColour> colours = new List<IColour>
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

            _repository.Setup(r => r.Add(newProduct)).Throws(new DatabaseException("Add fails"));
            _productService.Add(newProduct);
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
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void Delete_Fails_ThrowsException()
        {
            int productId = 1;
            _repository.Setup(r => r.Delete(productId)).Throws(new DatabaseException("Delete fails"));
            _productService.Delete(productId);
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

            List<IColour> colours = new List<IColour>
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
        [ExpectedException(typeof(Service_ObjectHandlingException))]

        public void Update_Fails_ThrowsException()
        {
            SetUpMocks();
            int productIdToUpdate = 1;

            Brand brand = new Brand();
            brand.Name = "Puma";

            Category category = new Category();
            category.Name = "Pants";

            Colour colour = new Colour();
            colour.Name = "Red";

            List<IColour> colours = new List<IColour>
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
            _repository.Setup(r => r.Update(updatedProduct)).Throws(new DatabaseException("Update fails"));
            _repository.Setup(r => r.Get(productIdToUpdate)).Returns(updatedProduct);

            _productService.Update(updatedProduct);
        }

        [TestMethod]
        public void GetFiltered_ByBrand()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            string brand = "Puma";
            Brand brandObj = new Brand(brand);
            ProductFilterDTO filter = new ProductFilterDTO() { Brand = brand};

            List<Product> products = _productService.GetFiltered(filter);

            Assert.AreEqual(2, products.Count);
            Assert.IsTrue(products.All(p => p.Brand.Equals(brandObj)));
        }

        [TestMethod]
        public void GetFiltered_ByCategory()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            string category = "T-Shirt";
            Category categoryObj = new Category(category);
            ProductFilterDTO filter = new ProductFilterDTO() { Category = category };

            List<Product> products = _productService.GetFiltered(filter);

            Assert.AreEqual(3, products.Count);
            Assert.IsTrue(products.All(p => p.Category.Equals(categoryObj)));
        }

        [TestMethod]
        public void GetFiltered_ByPrice()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            double maxPrice = 60;
            double minPrice = 50;
            ProductFilterDTO filter = new ProductFilterDTO() { MaximumPrice = maxPrice, MinimumPrice = minPrice };

            List<Product> products = _productService.GetFiltered(filter);

            Assert.AreEqual(2, products.Count);
            Assert.IsTrue(products.All(p => p.PriceUYU >= minPrice && p.PriceUYU <= maxPrice));
        }

        [TestMethod]
        public void GetFiltered_ByExcluded()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            bool excluded = true;
            ProductFilterDTO filter = new ProductFilterDTO() { ExcludedFromPromos = excluded};

            List<Product> products = _productService.GetFiltered(filter);

            Assert.AreEqual(2, products.Count);
            Assert.IsTrue(products.All(p => p.Exclude.Equals(filter.ExcludedFromPromos)));
        }

        [TestMethod]
        public void GetAll_FilterByVariosParameters()
        {
            _repository.Setup(r => r.GetAll()).Returns(_mockProducts);
            Category category = new Category("T-Shirt");
            Brand brand = new Brand("Nike");
            string name = "Other name";

            ProductFilterDTO filter = new ProductFilterDTO()
            {
                Category = category.Name,
                Brand = brand.Name,
                Name = name,
            };

            List<Product> products = _productService.GetFiltered(filter);

            Assert.AreEqual(0, products.Count);

            filter.Name = null;
            products = _productService.GetFiltered(filter);

            Assert.AreEqual(3, products.Count);
            Assert.IsTrue(products.All(p => p.Category.Equals(category) && p.Brand.Equals(brand)));
        }

        [TestMethod]
        [ExpectedException(typeof(Service_ObjectHandlingException))]
        public void GetFiltered_FailsAtGetAll_ThrowsException()
        {
            _repository.Setup(r => r.GetAll()).Throws(new DatabaseException("Get all fails"));
            Category category = new Category("T-Shirt");
            Brand brand = new Brand("Nike");
            string name = "Other name";
            ProductFilterDTO filters = new ProductFilterDTO()
            {
                Category = category.Name,
                Brand = brand.Name,
                Name = name
            };

            List<Product> products = _productService.GetFiltered(filters);
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
                Colours = new List<IColour>() { new Colour("Blue")},
                Exclude = true,
            },
            new Product
            {
                Name = "Name 2",
                Description = "Description 2",
                PriceUYU = 50.0,
                Brand = new Brand("Puma"),
                Category = new Category("Pants"),
                Colours = new List<IColour>() { new Colour("Green"), new Colour("Red") },
                Exclude = true,
            },
            new Product
            {
                Name = "Name 3",
                Description = "Description 3",
                PriceUYU = 20.0,
                Brand = new Brand("Nike"),
                Category = new Category("T-Shirt"),
                Colours = new List<IColour> { new Colour("Red") },
                Exclude = false,
            },

            new Product
            {
                Name = "Name 4",
                Description = "Description 4",
                PriceUYU = 100.0,
                Brand = new Brand("Puma"),
                Category = new Category("Pants"),
                Colours = new List<IColour> { new Colour("Green"), new Colour("Red") },
                Exclude = false,
            },

            new Product
            {
                Name = "Name 5",
                Description = "Description 5",
                PriceUYU = 60.0,
                Brand = new Brand("Nike"),
                Category = new Category("T-Shirt"),
                Colours = new List<IColour> { new Colour("Blue"), new Colour("Green") },
                Exclude = false,
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
