using DataAccess.Entities;
using DataAccess.Expcetions;
using Rest_Api.Models;
using Rest_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFCategoryService : IGetService<Category>
    {
        public EFCategoryService() { }

        public List<Category> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<CategoryEntity> entities = context.CategoryEntities.ToList();
                    List<Category> categories = entities.Select(c => CategoryEntity.FromEntity(c)).ToList();

                    return categories;
                }
                catch
                {
                    throw new DatabaseException("Error while getting all categories from database");
                }
            }

        }

        public Category? Get(string name)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    CategoryEntity category = context.CategoryEntities.First(p => p.Name == name);

                    return CategoryEntity.FromEntity(category);
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to get category " + name);
            }
        }
    }
}
