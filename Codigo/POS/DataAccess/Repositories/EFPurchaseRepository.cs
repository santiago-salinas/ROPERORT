using DataAccess.Entities;
using DataAccess.Expcetions;
using Microsoft.EntityFrameworkCore;
using Rest_Api.Models;
using DataAccessInterfaces;
namespace DataAccess.DatabaseServices
{
    public class EFPurchaseRepository : ICRUDRepository<Purchase>
    {
        private readonly EFContext _context;

        public EFPurchaseRepository(EFContext context)
        {
            _context = context;
        }
        public List<Purchase> GetAll(Func<Purchase, bool>? filter = null)
        {
            try
            {
                List<PurchaseEntity> entities = _context.PurchaseEntities
                    .Include(p => p.Items.Select(i => i.Product))
                    .Include(p => p.User)
                    .ToList();

                List<Purchase> purchases = entities.Select(p => PurchaseEntity.FromEntity(p)).ToList();


                if (filter != null)
                {
                    purchases = purchases.Where(p => filter(p)).ToList();
                }

                return purchases;

            }
            catch
            {
                throw new DatabaseException("Error while getting all purchases from database");
            }
        }

        public List<Purchase> GetPurchaseHistory(int id)
        {
            try
            {
                List<PurchaseEntity> entities = _context.PurchaseEntities
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
            }
            catch
            {
                throw new DatabaseException("Error while trying to update purchase " + purchase.Id);
            }
        }
    }
}
