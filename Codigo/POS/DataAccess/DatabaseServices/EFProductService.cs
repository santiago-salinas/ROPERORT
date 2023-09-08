using DataAccess.Entities;
using DataAccess.Expcetions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFProductService
    {
        public EFProductService() { }

        public List<ProductEntity> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<ProductEntity> products = context.ProductEntities
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .Include(p => p.ProductColors.Select(c => c.Colour))
                        .ToList();

                    return products;

                }catch
                {
                    throw new DatabaseException("Error while getting all products from database");
                }
            }
        }

        public ProductEntity? Get(string id) 
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    ProductEntity product = context.ProductEntities
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .Include(p => p.ProductColors.Select(c => c.Colour))
                        .First(p => p.Name == id);

                    return product;

                }
                catch
                {
                    throw new DatabaseException("Error while trying to get product with id " + id);
                }

            }

        }

        public void Add(ProductEntity entity)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.ProductEntities.Add(entity);
                    context.SaveChanges();
                }
            }
            catch
            {
            
                throw new DatabaseException("Error while trying to add product " + entity.Name);
            }
        }

        public void Delete(string id)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.ProductEntities.Remove(Get(id));
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to delete product with id " + id);
            }
        }

        public void Update(ProductEntity entity)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    context.ProductEntities.Update(entity);
                }
            }
            catch 
            {
                throw new DatabaseException("Error while trying to update product " + entity.Name);
            }
        }
    }
}
