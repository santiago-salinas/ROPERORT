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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchasedProductEntity>().HasKey(ppe => new { ppe.PurchaseId, ppe.ProductName });
            modelBuilder.Entity<AssignedRoles>().HasKey(ar => new { ar.RoleName, ar.UserEmail });
            modelBuilder.Entity<ProductColors>().HasKey(pc => new { pc.ProductName, pc.ColourName });
        }

        public DbSet<ProductEntity> ProductEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<PurchaseEntity> PurchaseEntities { get; set; }
        public DbSet<ColourEntity> ColourEntities { get; set;}
        public DbSet<BrandEntity> BrandEntities { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }

        public DbSet<AssignedRoles> AssignedRoles { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
    }
}