using DataAccess.Entities;
using Services.Interfaces;
using Services.Models;
using Services.Exceptions;



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
            catch (Exception ex)
            {
                throw new DatabaseException($"Unexpected exception while getting all categories : {ex.Message}");
            }
        }

        public Category? Get(string name)
        {
            try
            {
                CategoryEntity category = _context.CategoryEntities.First(p => p.Name == name);

                return CategoryEntity.FromEntity(category);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database operation exception while getting category " + name);
            }
        }
    }
}
