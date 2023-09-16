﻿using DataAccess.Entities;
using DataAccess.Expcetions;
using DataAccessInterfaces;
using Microsoft.EntityFrameworkCore;
using Rest_Api.Models;


namespace DataAccess.DatabaseServices
{
    public class EFProductRepository : ICRUDRepository<Product>
    {
        private readonly EFContext _context;
        public EFProductRepository(EFContext context)
        { _context = context; }

        public List<Product> GetAll(Func<Product, bool>? filter = null)
        {
            try
            {
                List<ProductEntity> entities = _context.ProductEntities
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Colours)
                        .ThenInclude(c => c.Colour)
                    .ToList();


                List<Product> products = entities.Select(p => ProductEntity.FromEntity(p)).ToList();

                if (filter != null)
                {
                    products = products.Where(p => filter(p)).ToList();
                }

                return products;

            }
            catch
            {
                throw new DatabaseException("Error while getting all products from database");
            }
        }

        public Product? Get(int id)
        {
            try
            {
                ProductEntity product = _context.ProductEntities
                    .Include(p => p.Brand)
                    .Include(p => p.Category)
                    .Include(p => p.Colours)
                        .ThenInclude(c=> c.Colour)
                    .First(p => p.Id == id);

                return ProductEntity.FromEntity(product);

            }
            catch(InvalidOperationException) { }
            {
                return null;
            }
        }

        public void Add(Product product)
        {
            try
            {
                ProductEntity entity = ProductEntity.FromModel(product, _context);
                _context.ProductEntities.Add(entity);
                _context.SaveChanges();
            }
            catch
            {

                throw new DatabaseException("Error while trying to add product " + product.Name);
            }
        }

        public void Delete(int id)
        {
            try
            {

                ProductEntity entity = _context.ProductEntities.First(p => p.Id == id);
                _context.ProductEntities.Remove(entity);
                _context.SaveChanges();
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
                ProductEntity entity = ProductEntity.FromModel(product, _context);
                _context.ProductEntities.Update(entity);
                _context.SaveChanges();
            }
            catch
            {
                throw new DatabaseException("Error while trying to update product " + product.Name);
            }
        }
    }
}
