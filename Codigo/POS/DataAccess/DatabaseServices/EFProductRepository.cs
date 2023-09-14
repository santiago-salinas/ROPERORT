using DataAccess.Entities;
using DataAccess.Expcetions;
using DataAccessInterfaces;
using Microsoft.EntityFrameworkCore;
using Rest_Api.Models;
using Rest_Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DatabaseServices
{
    public class EFProductRepository : IProductRepository
    {
        private int _nextAvailableId;
        public EFProductRepository()  
        {
            using (EFContext context = new EFContext())
            {


                if (context.ProductEntities.Any())
                {
                    _nextAvailableId = context.ProductEntities.Max(x => x.Id) + 1;
                }
                else
                {
                    _nextAvailableId = 1;
                }
            }
        }

        public int NextId { get { return _nextAvailableId; } }

        public List<Product> GetAll()
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    List<ProductEntity> entities = context.ProductEntities
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .Include(p => p.Colours)
                        .ToList();
                    entities.ForEach(p =>
                    {
                        p.Colours = p.Colours.Select(c1 =>
                        {
                            c1.Colour = context.ColourEntities.First(c2 => c2.Name == c1.ColourName);
                            return c1;
                        }).ToList();
                    });

                    List<Product> products = entities.Select(p => ProductEntity.FromEntity(p)).ToList();

                    return products;

                }catch
                {
                    throw new DatabaseException("Error while getting all products from database");
                }
            }
        }

        public Product? Get(int id) 
        {
            using (EFContext context = new EFContext())
            {
                try
                {
                    ProductEntity product = context.ProductEntities
                        .Include(p => p.Brand)
                        .Include(p => p.Category)
                        .Include(p => p.Colours.Select(c => c.Colour))
                        .First(p => p.Id == id);

                    return ProductEntity.FromEntity(product);

                }
                catch
                {
                    throw new DatabaseException("Error while trying to get product with id " + id);
                }

            }
        }

        public void Add(Product product)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    ProductEntity entity = ProductEntity.FromModel(product,context);
                    //entity.Id = _nextAvailableId;
                    context.ProductEntities.Add(entity);
                    context.SaveChanges();
                }
            }
            catch
            {
            
                throw new DatabaseException("Error while trying to add product " + product.Name);
            }
            _nextAvailableId++;
        }

        public void Delete(int id)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    ProductEntity entity = context.ProductEntities.First(p => p.Id == id);
                    context.ProductEntities.Remove(entity);
                }

            }
            catch
            {
                throw new DatabaseException("Error while trying to delete product with id " + id);
            }
        }

        public void Update(Product product)
        {
            try
            {
                using (EFContext context = new EFContext())
                {
                    ProductEntity entity = ProductEntity.FromModel(product,context);
                    context.ProductEntities.Update(entity);
                }
            }
            catch 
            {
                throw new DatabaseException("Error while trying to update product " + product.Name);
            }
        }
    }
}
