using Services.Exceptions;
using Services.Interfaces;
using Services.Models;

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

    public List<Product> GetAll()
    {
        try
        {
            return _productRepository.GetAll();
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public Product? Get(int id)
    {
        try
        {
            return _productRepository.Get(id);
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public void Add(Product product)
    {
        CheckProductParametersAreValid(product);
        try
        {
            _productRepository.Add(product);
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public void Delete(int id)
    {
        try
        {
            _productRepository.Delete(id);

        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public void Update(Product product)
    {
        CheckProductParametersAreValid(product);

        try
        {
            _productRepository.Update(product);

        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }

    public List<Product> GetFiltered(Category? category = null, Brand? brand = null, string? name = null)
    {
        IEnumerable<Product> result;
        try
        {
            result =  _productRepository.GetAll();
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }


        if (category != null)
        {
            result = result.Where(p => p.Category.Equals(category));
        }
        if (brand != null)
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
    private bool BrandExists(Brand brand)
    {
        try
        {
            return _brandRepository.Get(brand.Name) != null;
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }
    private bool CategoryExists(Category category)
    {
        try
        {
            return _categoryRepository.Get(category.Name) != null;
        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
    }
    private bool ColoursExist(List<Colour> colours)
    {
        try
        {
            foreach (Colour colour in colours)
            {

                if (_colourRepository.Get(colour.Name) == null)
                {
                    return false;
                }
            }

        }
        catch (DatabaseException ex)
        {
            throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
        }
        return true;
    }
}