using DataAccess.Entities;
using DataAccess.Expcetions;
using Rest_Api.Models;
using DataAccessInterfaces;


namespace DataAccess.DatabaseServices
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
