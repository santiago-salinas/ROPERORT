using DataAccess.Entities;
using DataAccess.Expcetions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFBrandService
    {
        public EFBrandService() { }

        public List<BrandEntity> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<BrandEntity> brands = context.BrandEntities.ToList();

                    return brands;

                }
                catch
                {
                    throw new DatabaseException("Error while getting all brands from database");
                }
            }

        }

        public BrandEntity? Get(string name)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    BrandEntity brand = context.BrandEntities.First(p => p.Name == name);

                    return brand;
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to get brand " + name);
            }
        }
    }
}
