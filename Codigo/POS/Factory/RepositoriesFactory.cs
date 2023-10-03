using DataAccess;
using DataAccess.Repositories;
using Services.Interfaces;
using Services.Models;

namespace Factory
{
    public class RepositoriesFactory
    {
        private EFContext _context = new EFContext();

        public ICRUDRepository<Product> ProductRepository { get; set; }
        public ICRUDRepository<User> UserRepository { get; set; }
        public ICRUDRepository<Purchase> PurchaseRepository { get; set; }
        public IGetRepository<Brand> BrandRepository { get; set; }
        public IGetRepository<Colour> ColourRepository { get; set; }
        public IGetRepository<Category> CategoryRepository { get; set; }
        public IGetRepository<Role> RoleRepository { get; set; }

        public void SetupRepositories()
        {
            ProductRepository = new EFProductRepository(_context);
            UserRepository = new EFUserRepository(_context);
            PurchaseRepository = new EFPurchaseRepository(_context);
            BrandRepository = new EFBrandRepository(_context);
            ColourRepository = new EFColourRepository(_context);
            CategoryRepository = new EFCategoryRepository(_context);
            RoleRepository = new EFRolesRepository(_context);
        }
    }
}