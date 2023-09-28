using DataAccess.Entities;
using Services.Interfaces;
using Services.Models;
using Services.Exceptions;


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
            catch (Exception ex)
            {
                throw new DatabaseException($"Unexpected exception while getting all brands : {ex.Message}");
            }
        }

        public Brand? Get(string name)
        {
            try
            {
                BrandEntity brand = _context.BrandEntities.First(p => p.Name == name);
                return BrandEntity.FromEntity(brand);
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }
    }
}
