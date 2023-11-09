using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;
using Services.Exceptions;


namespace DataAccess.Repositories
{
    public class EFProductRepository : ICRUDRepository<Product>
    {
        private readonly EFContext _context;
        public EFProductRepository(EFContext context)
        {
            _context = context;
        }

        public List<Product> GetAll()
        {
            try
            {
                List<ProductEntity> entities = _context.ProductEntities
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Colours)
                        .ThenInclude(c => c.Colour)
                    .ToList();


                List<Product> products = entities.Select(p => ProductEntity.FromEntity(p)).ToList();

                return products;

            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Unexpected exception while getting all products : {ex.Message}");
            }
        }

        public Product? Get(int id)
        {
            try
            {
                ProductEntity product = _context.ProductEntities
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Colours)
                        .ThenInclude(c => c.Colour)
                    .First(p => p.Id == id);

                return ProductEntity.FromEntity(product);

            }
            catch (InvalidOperationException ex)
            {                
                return null;
            }
        }

        public void Add(Product product)
        {
            if (NameAlreadyInUse(product.Name))
            {
                throw new DatabaseException("Product name already in use");
            }

            try
            {
                ProductEntity entity = ProductEntity.FromModel(product, _context);
                entity.Id = null;
                _context.ProductEntities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while adding product " + product.Name);
            }
            catch(InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException("Exception while converting product from model: " + ex.InnerException.Message);
                }
                else
                {
                    throw new DatabaseException("Exception while converting product from model: " + ex.Message);
                }
            }
        }

        public void Delete(int id)
        {
            try
            {
                ProductEntity? entity = _context.ProductEntities.FirstOrDefault(p => p.Id == id);
                _context.ProductEntities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while deleting product with id: " + id);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                else
                {
                    throw new DatabaseException(ex.Message);
                }
            }
        }

        public void Update(Product product)
        {
            try
            {
                ProductEntity newEntity = ProductEntity.FromModel(product, _context);
                ProductEntity oldEntity = _context.ProductEntities.Find(product.Id);

                if (oldEntity.Name != newEntity.Name && NameAlreadyInUse(newEntity.Name))
                {
                    throw new DatabaseException("New product name already in use");
                }

                oldEntity.Colours = newEntity.Colours;
                oldEntity.Brand = newEntity.Brand;
                oldEntity.Category = newEntity.Category;
                oldEntity.Price = newEntity.Price;
                oldEntity.Description = newEntity.Description;
                oldEntity.Name = newEntity.Name;
                oldEntity.Stock = newEntity.Stock;
                oldEntity.Exclude = newEntity.Exclude;

                _context.ProductEntities.Update(oldEntity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while updating product " + product.Name);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException("Exception while converting product from model: " + ex.InnerException.Message);
                }
                else
                {
                    throw new DatabaseException("Exception while converting product from model: " + ex.Message);
                }
            }
        }

        private bool NameAlreadyInUse(string name)
        {
            return _context.ProductEntities.Any(p => p.Name == name);
        }
    }
}
