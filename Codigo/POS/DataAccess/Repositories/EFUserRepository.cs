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
                return null;
            }
        }

        public void Add(User user)
        {
            try
            {
                if(!UserExists(user.Email))
                {
                    UserEntity entity = UserEntity.FromModel(user);
                    _context.UserEntities.Add(entity);
                    _context.SaveChanges();
                }
                else
                {
                    throw new DatabaseException("User already exists");
                }
            }
            catch(DbUpdateException ex)
            {
                if(ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while adding " + user.Email);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException("Invalid operation exception: " + ex.InnerException.Message);
                }
                else
                {
                    throw new DatabaseException("Invalid operation exception: " + ex.Message);
                }
            }
        }

        public void Delete(int id)
        {
            try
            {
                UserEntity? entity = _context.UserEntities.FirstOrDefault(p => p.Id == id);
                if (entity != null)
                {
                    _context.UserEntities.Remove(entity);
                    _context.SaveChanges();
                }
                else
                {
                    throw new DatabaseException("User to delete does not exist");
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while removing user with id " + id);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                else
                {
                    throw new DatabaseException(ex.Message);
                }
            }
        }

        public void Update(User user)
        {
            try
            {
                if(UserExists(user.Email))
                {
                    UserEntity entity = UserEntity.FromModel(user);
                    _context.UserEntities.Update(entity);
                    _context.SaveChanges();
                }
                else
                {
                    throw new DatabaseException("User to update does not exist");
                }                
            }
            catch(DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while updating user " + user.Email);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException("Invalid operation exception: " + ex.InnerException.Message);
                }
                else
                {
                    throw new DatabaseException("Invalid operation exception: " + ex.Message);
                }
            }
        }

        private bool UserExists(string email)
        {
            return _context.UserEntities.Any(x => x.Email == email);
        }
    }
}
