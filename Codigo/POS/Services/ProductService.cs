using System.Xml.Linq;
using Models;
using Rest_Api.Services.Exceptions;
using Rest_Api.Interfaces;
using Rest_Api.Services;
using DataAccessInterfaces;

namespace Services;

public class ProductService : IProductService
{
    
    private readonly ICRUDRepository<Product> _productRepository;
    private IGetRepository<Colour> _colourRepository;
    private IGetRepository<Brand> _brandRepository;
    private IGetRepository<Category> _categoryRepository;

    public IGetRepository<Colour> ColourRepository { set { _colourRepository = value; } }
    public IGetRepository<Brand> BrandRepository { set { _brandRepository = value; } }
    public IGetRepository<Category> CategoryRepository { set { _categoryRepository = value; } }
    public ProductService(ICRUDRepository<Product> repository)
    {
        _productRepository = repository;
    }

    public List<Product> GetAll() => _productRepository.GetAll();

    public Product? Get(int id) => _productRepository.Get(id);

    public void Add(Product product)
    {
        CheckProductParametersAreValid(product);
        _productRepository.Add(product);
    }

    public void Delete(int id)
    {
        _productRepository.Delete(id);
    }

    public void Update(Product product)
    {
        CheckProductParametersAreValid(product);
        _productRepository.Update(product);
    }

    public List<Product> GetFiltered(Category? category = null, Brand? brand = null, string? name = null)
    {
        IEnumerable<Product> result = GetAll();
        if(category != null)
        {
            result = result.Where(p => p.Category.Equals(category));
        }
        if(brand != null)
        {
            result = result.Where(p => p.Brand.Equals(brand));
        }
        if (name != null)
        {
            result = result.Where(p => p.Name.Contains(name));
        }

        return result.ToList();
    }


    private void CheckProductParametersAreValid(Product product)
    {
        if (!BrandExists(product.Brand)) { throw new Service_ArgumentException("Brand does not exist"); }
        if (!CategoryExists(product.Category)) { throw new Service_ArgumentException("Category does not exist"); }
        if (!ColoursExist(product.Colours)) { throw new Service_ArgumentException("One of the Colours does not exist"); }
    }
    private bool BrandExists(Brand brand) => _brandRepository.Get(brand.Name) != null;
    private bool CategoryExists(Category category) => _categoryRepository.Get(category.Name) != null;
    private bool ColoursExist(List<Colour> colours)
    {
        foreach (Colour colour in colours)
        {
            if (_colourRepository.Get(colour.Name) == null)
            {
                return false;
            }
        }
        return true;
    }
}