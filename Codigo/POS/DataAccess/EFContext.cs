using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccess
{
    public class EFContext : DbContext
    {
        private readonly bool _useInMemoryDatabase;
        public EFContext(DbContextOptions<EFContext>? options = null, bool useInMemoryDB = false) : base()
        {
            _useInMemoryDatabase = useInMemoryDB;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_useInMemoryDatabase)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
            }
            else
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=ECommerceDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<ProductEntity>()
                .HasIndex(e => e.Name)
                .IsUnique();

            modelBuilder.Entity<ProductEntity>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserEntity>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<PurchasedProductEntity>().HasKey(ppe => new { ppe.PurchaseId, ppe.ProductId });
            modelBuilder.Entity<AssignedRoles>().HasKey(ar => new { ar.RoleName, ar.UserId });
            modelBuilder.Entity<ProductColors>().HasKey(pc => new { pc.ProductId, pc.ColourName });
        }

        public DbSet<ProductEntity> ProductEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<PurchaseEntity> PurchaseEntities { get; set; }
        public DbSet<ColourEntity> ColourEntities { get; set; }
        public DbSet<BrandEntity> BrandEntities { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<RoleEntity> RoleEntities { get; set; }

        public DbSet<AssignedRoles> AssignedRoles { get; set; }
        public DbSet<ProductColors> ProductColors { get; set; }
        public DbSet<PaymentMethodEntity> PaymentMethods { get; set; }
    }
}