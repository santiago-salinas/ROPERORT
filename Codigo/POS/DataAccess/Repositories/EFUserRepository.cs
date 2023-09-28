using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;
using Services.Exceptions;


namespace DataAccess.Repositories
{
    public class EFUserRepository : ICRUDRepository<User>
    {
        private readonly EFContext _context;
        public EFUserRepository(EFContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            try
            {
                List<UserEntity> entities = _context.UserEntities
                    .Include(u => u.Roles)
                    .ToList();

                List<User> users = entities.Select(p => UserEntity.FromEntity(p)).ToList();

                return users;

            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Unexpected exception while getting all users: {ex.Message}");
            }

        }

        public User? Get(int id)
        {
            try
            {
                UserEntity user = _context.UserEntities
                    .Include(u => u.Roles)
                    .First(u => u.Id == id);

                return UserEntity.FromEntity(user);

            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database operation exception while getting user with id: " + id);
            }
        }

        public void Add(User user)
        {
            try
            {
                UserEntity entity = UserEntity.FromModel(user);
                _context.UserEntities.Add(entity);
                _context.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                if(ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while adding " + user.Email);
            }
        }

        public void Delete(int id)
        {
            try
            {
                UserEntity entity = _context.UserEntities.First(p => p.Id == id);
                _context.UserEntities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while removing user with id " + id);
            }
        }

        public void Update(User user)
        {
            try
            {
                UserEntity entity = UserEntity.FromModel(user);
                _context.UserEntities.Update(entity);
                _context.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while updating user " + user.Email);
            }
        }
    }
}
