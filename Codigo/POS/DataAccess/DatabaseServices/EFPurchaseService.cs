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
    public class EFPurchaseService
    {
        public EFPurchaseService() 
        {
            using (EFContext context = new EFContext())
            {
                _nextId = context.PurchaseEntities.Max(p => p.Id) + 1;


            }
        }

        private int _nextId;

        public List<PurchaseEntity> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<PurchaseEntity> purchases = context.PurchaseEntities
                        .Include(p => p.Items.Select(i => i.Product))
                        .Include(p => p.User)
                        .ToList();

                    return purchases;

                }
                catch
                {
                    throw new DatabaseException("Error while getting all purchases from database");
                }
            }
        }

        public List<PurchaseEntity> GetPurchaseHistory(string email)
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<PurchaseEntity> purchases = context.PurchaseEntities
                        .Include(p => p.Items.Select(i => i.Product))
                        .Include(p => p.User)
                        .Where(p => p.User.Email == email)
                        .ToList();

                    return purchases;

                }
                catch
                {
                    throw new DatabaseException("Error while getting purchase history from user " + email);
                }
            }
        }

        public PurchaseEntity? Get(int id)
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    PurchaseEntity purchase = context.PurchaseEntities
                        .Include(p => p.Items.Select(i => i.Product))
                        .Include(p => p.User)
                        .First(p => p.Id == id);

                    return purchase;

                }
                catch
                {
                    throw new DatabaseException("Error while trying to get purchase with id " + id);
                }

            }

        }

        public void Add(PurchaseEntity entity)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.PurchaseEntities.Add(entity);
                    context.SaveChanges();
                }
            }
            catch
            {

                throw new DatabaseException("Error while trying to add purchase " + entity.Id);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.PurchaseEntities.Remove(Get(id));
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to delete purchase with id " + id);
            }
        }

        public void Update(PurchaseEntity entity)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.PurchaseEntities.Update(entity);
                }
            }
            catch
            {
                throw new DatabaseException("Error while trying to update purchase " + entity.Id);
            }
        }
    }
}
