using DataAccess.Entities;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;

namespace DataAccess.Repositories
{
    public class EFPurchaseRepository : ICRUDRepository<Purchase>
    {
        private readonly EFContext _context;

        public EFPurchaseRepository(EFContext context)
        {
            _context = context;
        }
        public List<Purchase> GetAll()
        {
            try
            {
                List<PurchaseEntity> entities = _context.PurchaseEntities
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
        public Purchase? Get(int id)
        {

            try
            {
                PurchaseEntity entity = _context.PurchaseEntities
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

        public void Add(Purchase purchase)
        {
            try
            {
                PurchaseEntity entity = PurchaseEntity.FromModel(purchase, _context);
                _context.PurchaseEntities.Add(entity);
                _context.SaveChanges();
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
                PurchaseEntity entity = _context.PurchaseEntities.First(p => p.Id == id);
                _context.PurchaseEntities.Remove(entity);
                _context.SaveChanges();
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
                PurchaseEntity entity = PurchaseEntity.FromModel(purchase, _context);
                _context.PurchaseEntities.Update(entity);
                _context.SaveChanges();
            }
            catch
            {
                throw new DatabaseException("Error while trying to update purchase " + purchase.Id);
            }
        }
    }
}
