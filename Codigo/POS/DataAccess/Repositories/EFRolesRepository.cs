using DataAccess.Entities;
using Services.Exceptions;
using Services.Interfaces;
using Services.Models;



namespace DataAccess.Repositories
{
    public class EFRolesRepository : IGetRepository<Role>
    {
        private readonly EFContext _context;
        public EFRolesRepository(EFContext context) { _context = context; }

        public List<Role> GetAll()
        {
            try
            {
                List<RoleEntity> entities = _context.RoleEntities.ToList();
                List<Role> roles = entities.Select(r => RoleEntity.FromEntity(r)).ToList();

                return roles;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Unexpected exception while getting all roles : {ex.Message}");
            }
        }

        public Role? Get(string name)
        {
            try
            {
                RoleEntity role = _context.RoleEntities.First(r => r.Name == name);

                return RoleEntity.FromEntity(role);
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }
    }
}
