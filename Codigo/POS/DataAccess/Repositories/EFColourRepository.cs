using DataAccess.Entities;
using Services.Interfaces;
using Services.Models;
using Services.Exceptions;


namespace DataAccess.Repositories

{
    public class EFColourRepository : IGetRepository<Colour>
    {
        private readonly EFContext _context;
        public EFColourRepository(EFContext context)
        {
            _context = context;
        }

        public List<Colour> GetAll()
        {
            try
            {
                List<ColourEntity> entities = _context.ColourEntities.ToList();
                List<Colour> colours = entities.Select(c => ColourEntity.FromEntity(c)).ToList();

                return colours;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Unexpected exception while getting all colours : {ex.Message}");
            }
        }

        public Colour? Get(string name)
        {
            try
            {
                ColourEntity entity = _context.ColourEntities.First(p => p.Name == name);

                return ColourEntity.FromEntity(entity);
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }
    }
}
