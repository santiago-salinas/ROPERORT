using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models;
using Services.Exceptions;


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
                List<PurchaseEntity> purchaseEntities = _context.PurchaseEntities
                    .Include(p => p.User)
                    .Include(p => p.Items)
                        .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Brand)
                    .Include(p => p.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Category)
                        .Include(p => p.Items)
                            .ThenInclude(i => i.Product)
                                .ThenInclude(p => p.Colours)
                                    .ThenInclude(c => c.Colour)
                        .ToList();

                List<Purchase> purchases = purchaseEntities.Select(p => PurchaseEntity.FromEntity(p)).ToList();

                return purchases;
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Unexpected exception while getting all purchases : {ex.Message}");
            }
        }
        public Purchase? Get(int id)
        {
            try
            {
                PurchaseEntity purchaseEntity = _context.PurchaseEntities
                    .Where(p => p.Id == id)
                    .Include(p => p.User)
                    .Include(p => p.Items)
                        .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Brand)
                    .Include(p => p.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Category)
                        .Include(p => p.Items)
                            .ThenInclude(i => i.Product)
                                .ThenInclude(p => p.Colours)
                                    .ThenInclude(c => c.Colour)
                        .First();

                Purchase purchase = PurchaseEntity.FromEntity(purchaseEntity);

                return purchase;
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }

        public void Add(Purchase purchase)
        {
            try
            {
                var paymentModel = purchase.PaymentMethod;
                var possiblePayment = _context.PaymentMethods.FirstOrDefault(p => paymentModel.Id.Equals(p.Id));
                if (possiblePayment == null)
                {
                    PaymentMethodEntity paymentEntity = PaymentMethodEntity.FromModel(paymentModel, _context);
                    _context.PaymentMethods.Add(paymentEntity);
                    _context.SaveChanges();
                }

                PurchaseEntity entity = PurchaseEntity.FromModel(purchase, _context);
                _context.PurchaseEntities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while adding purchase with Id: " + purchase.Id);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException("Exception while converting purchase from model: " + ex.InnerException.Message);
                }
                else
                {
                    throw new DatabaseException("Exception while converting purchase from model: " + ex.Message);
                }
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
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while deleting purchase with Id: " + id);
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

        public void Update(Purchase purchase)
        {
            try
            {
                PurchaseEntity entity = PurchaseEntity.FromModel(purchase, _context);
                _context.PurchaseEntities.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException(ex.InnerException.Message);
                }
                throw new DatabaseException("Database update exception while updating purchase with Id: " + purchase.Id);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw new DatabaseException("Exception while converting purchase from model: " + ex.InnerException.Message);
                }
                else
                {
                    throw new DatabaseException("Exception while converting purchase from model: " + ex.Message);
                }
            }
        }
    }
}
