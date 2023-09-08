using DataAccess.Entities;
using DataAccess.Expcetions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFUserService
    {
        public EFUserService() { }

        public List<UserEntity> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<UserEntity> users = context.UserEntities
                        .Include(u => u.Roles)
                        .ToList();

                    return users;

                }
                catch
                {
                    throw new DatabaseException("Error while getting all users from database");
                }
            }
        }

        public UserEntity? Get(string email)
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    UserEntity user = context.UserEntities
                        .Include(u => u.Roles)
                        .First(u => u.Email == email);

                    return user;

                }
                catch
                {
                    throw new DatabaseException("Error while trying to get user with email" + email);
                }
            }
        }

        public void Add(UserEntity entity)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.UserEntities.Add(entity);
                    context.SaveChanges();
                }
            }
            catch
            {

                throw new DatabaseException("Error while tryinh to add user " + entity.Email);
            }
        }

        public void Delete(string email)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.UserEntities.Remove(Get(email));
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to delete user with email" + email);
            }
        }

        public void Update(UserEntity entity)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.UserEntities.Update(entity);
                }
            }
            catch
            {
                throw new DatabaseException("Error while trying to update user " + entity.Email);
            }
        }
    }
}
