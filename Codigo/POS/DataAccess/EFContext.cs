using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccess
{
    public class EFContext : DbContext

    {
        public EFContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ECommerceDB;Trusted_Connection=True;");
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }*/

        public DbSet<ProductEntity> ProductEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<PurchaseEntity> PurchaseEntities { get; set; }
        public DbSet<ColourEntity> ColourEnityEntities { get; set;}
        public DbSet<BrandEntity> BrandEntities { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
    }
}