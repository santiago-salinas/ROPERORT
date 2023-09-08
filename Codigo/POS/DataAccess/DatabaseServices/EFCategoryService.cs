using DataAccess.Entities;
using DataAccess.Expcetions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFCategoryService
    {
        public EFCategoryService() { }

        public List<CategoryEntity> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<CategoryEntity> colours = context.CategoryEntities.ToList();

                    return colours;

                }
                catch
                {
                    throw new DatabaseException("Error while getting all categories from database");
                }
            }

        }

        public CategoryEntity? Get(string name)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    CategoryEntity colour = context.CategoryEntities.First(p => p.Name == name);

                    return colour;
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to get category " + name);
            }
        }
    }
}
