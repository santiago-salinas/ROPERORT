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
    public class EFBrandService : IGetService<Brand>
    {
        public EFBrandService() { }

        public List<Brand> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<BrandEntity> entities = context.BrandEntities.ToList();
                    List<Brand> brands = entities.Select(c => BrandEntity.FromEntity(c)).ToList();

                    return brands;

                }
                catch
                {
                    throw new DatabaseException("Error while getting all brands from database");
                }
            }
        }

        public Brand? Get(string name)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    BrandEntity brand = context.BrandEntities.First(p => p.Name == name);

                    return BrandEntity.FromEntity(brand);
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to get brand " + name);
            }
        }
    }
}
