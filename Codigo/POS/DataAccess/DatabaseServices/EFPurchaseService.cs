using DataAccess.Entities;
using DataAccess.Expcetions;
using Microsoft.EntityFrameworkCore;
using Rest_Api.Models;
using Rest_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFPurchaseService : ICRUDService<Purchase>
    {
        public EFPurchaseService() 
        {
            using (EFContext context = new EFContext())
            {
                _nextId = context.PurchaseEntities.Max(p => p.Id) + 1;
            }
        }

        private int _nextId;

        public List<Purchase> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<PurchaseEntity> entities = context.PurchaseEntities
                        .Include(p => p.Items.Select(i => i.Product))
                        .Include(p => p.User)
                        .ToList();

                    List<Purchase> purchases = entities.Select(p => PurchaseEntity.FromEntity(p)).ToList(); 

                    return purchases;

                }
                catch
                {
                    throw new DatabaseException("Error while getting all purchases from database");
                }
            }
        }

        public List<Purchase> GetPurchaseHistory(int id)
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<PurchaseEntity> entities = context.PurchaseEntities
                        .Include(p => p.Items.Select(i => i.Product))
                        .Include(p => p.User)
                        .Where(p => p.User.Id == id)
                        .ToList();

                    List<Purchase> purchases = entities.Select(p => PurchaseEntity.FromEntity(p)).ToList();
                    return purchases;

                }
                catch
                {
                    throw new DatabaseException("Error while getting purchase history from user " + id);
                }
            }
        }

        public Purchase? Get(int id)
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    PurchaseEntity entity = context.PurchaseEntities
                        .Include(p => p.Items.Select(i => i.Product))
                        .Include(p => p.User)
                        .First(p => p.Id == id);

                    Purchase purchase = PurchaseEntity.FromEntity(entity);

                    return purchase;

                }
                catch
                {
                    throw new DatabaseException("Error while trying to get purchase with id " + id);
                }

            }

        }

        public void Add(Purchase purchase)
        {
            try
            {
                using (EFContext context = new EFContext())
                {

                    PurchaseEntity entity = PurchaseEntity.FromModel(purchase);
                    context.PurchaseEntities.Add(entity);
                    context.SaveChanges();
                }
            }
            catch
            {

                throw new DatabaseException("Error while trying to add purchase " + purchase.Id);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    PurchaseEntity entity = context.PurchaseEntities.First(p => p.Id == id);
                    context.PurchaseEntities.Remove(entity);
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to delete purchase with id " + id);
            }
        }

        public void Update(Purchase purchase)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    PurchaseEntity entity = PurchaseEntity.FromModel(purchase);
                    context.PurchaseEntities.Update(entity);
                }
            }
            catch
            {
                throw new DatabaseException("Error while trying to update purchase " + purchase.Id);
            }
        }
    }
}
