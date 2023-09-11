using DataAccess.Entities;
using DataAccess.Expcetions;
using Microsoft.EntityFrameworkCore;
using Rest_Api.Services;
using Rest_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFUserService : ICRUDService<User>
    {
        public EFUserService() { }

        public List<User> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<UserEntity> entities = context.UserEntities
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
        }

        public User? Get(int id)
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    UserEntity user = context.UserEntities
                        .Include(u => u.Roles)
                        .First(u => u.Id == id);

                    return UserEntity.FromEntity(user);

                }
                catch
                {
                    throw new DatabaseException("Error while trying to get user with id" + id);
                }
            }
        }

        public void Add(User user)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    UserEntity entity = UserEntity.FromModel(user);

                    context.UserEntities.Add(entity);
                    context.SaveChanges();
                }
            }
            catch
            {

                throw new DatabaseException("Error while tryinh to add user " + user.Email);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    UserEntity entity = context.UserEntities.First(p => p.Id == id);
                    context.UserEntities.Remove(entity);
                }

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

                using (EFContext context = new EFContext())
                {
                    context.UserEntities.Update(entity);
                }
            }
            catch
            {
                throw new DatabaseException("Error while trying to update user " + user.Email);
            }
        }
    }
}
