using DataAccess.Entities;
using DataAccess.Exceptions;
using Services.Interfaces;
using Services.Models;


namespace DataAccess.Repositories
{
    public class EFBrandRepository : IGetRepository<Brand>
    {
        private readonly EFContext _context;
        public EFBrandRepository(EFContext context) { _context = context; }

        public List<Brand> GetAll()
        {
            try
            {
                List<BrandEntity> entities = _context.BrandEntities.ToList();
                List<Brand> brands = entities.Select(c => BrandEntity.FromEntity(c)).ToList();

                return brands;
            }
            catch
            {
                throw new DatabaseException("Error while getting all brands from database");
            }
        }

        public Brand? Get(string name)
        {
            try
            {
                BrandEntity brand = _context.BrandEntities.First(p => p.Name == name);
                return BrandEntity.FromEntity(brand);
            }
            catch
            {
                throw new DatabaseException("Error while trying to get brand " + name);
            }
        }
    }
}
