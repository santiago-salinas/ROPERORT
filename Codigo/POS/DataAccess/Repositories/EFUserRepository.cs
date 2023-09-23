using DataAccess.Entities;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Interfaces;

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
            catch
            {
                throw new DatabaseException("Error while getting all users from database");
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
            catch
            {
                throw new DatabaseException("Error while trying to get user with id" + id);
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
            catch
            {
                throw new DatabaseException("Error while trying to add user " + user.Email);
            }
        }

        public void Delete(int id)
        {
            try
            {
                UserEntity entity = _context.UserEntities.First(p => p.Id == id);
                _context.UserEntities.Remove(entity);
            }
            catch
            {
                throw new DatabaseException("Error while trying to delete user with id" + id);
            }
        }

        public void Update(User user)
        {
            try
            {
                UserEntity entity = UserEntity.FromModel(user);
                _context.UserEntities.Update(entity);
            }
            catch
            {
                throw new DatabaseException("Error while trying to update user " + user.Email);
            }
        }
    }
}
