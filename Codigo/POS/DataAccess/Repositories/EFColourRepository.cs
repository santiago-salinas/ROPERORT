using DataAccess.Entities;
using DataAccess.Expcetions;
using Microsoft.EntityFrameworkCore;
using Rest_Api.Models;
using DataAccessInterfaces;

namespace DataAccess.DatabaseServices
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
            catch
            {
                throw new DatabaseException("Error while getting all colours from database");
            }

        }

        public Colour? Get(string name)
        {
            try
            {

                ColourEntity entity = _context.ColourEntities.First(p => p.Name == name);

                return ColourEntity.FromEntity(entity);


            }
            catch
            {
                throw new DatabaseException("Error while trying to get colour " + name);
            }
        }
    }
}
