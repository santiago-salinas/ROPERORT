using DataAccess.Entities;
using DataAccess.Exceptions;
using Services.Interfaces;
using Services.Models;


namespace DataAccess.Repositories
{
    public class EFCategoryRepository : IGetRepository<Category>
    {
        private readonly EFContext _context;
        public EFCategoryRepository(EFContext context) { _context = context; }

        public List<Category> GetAll()
        {
            try
            {
                List<CategoryEntity> entities = _context.CategoryEntities.ToList();
                List<Category> categories = entities.Select(c => CategoryEntity.FromEntity(c)).ToList();

                return categories;
            }
            catch
            {
                throw new DatabaseException("Error while getting all categories from database");
            }
        }

        public Category? Get(string name)
        {
            try
            {
                CategoryEntity category = _context.CategoryEntities.First(p => p.Name == name);

                return CategoryEntity.FromEntity(category);
            }
            catch
            {
                throw new DatabaseException("Error while trying to get category " + name);
            }
        }
    }
}
