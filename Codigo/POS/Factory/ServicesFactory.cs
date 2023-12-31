﻿using Services;
using Services.Interfaces;
using Services.Models;

namespace Factory
{
    public class ServicesFactory
    {
        public ServicesFactory(RepositoriesFactory repositories)
        {
            RepositoriesFactory = repositories;
        }
        public RepositoriesFactory RepositoriesFactory { get; set; }
        public IProductService ProductService { get; set; }
        public IUserService UserService { get; set; }
        public IPurchaseService PurchaseService { get; set; }
        public IGetService<Brand> BrandService { get; set; }
        public IGetService<Colour> ColourService { get; set; }
        public IGetService<Category> CategoryService { get; set; }
        public IPromoService PromoService { get; set; }


        public void SetupServices()
        {
            PromoService = new PromoService();
            ProductService = new ProductService(RepositoriesFactory.ProductRepository)
            {
                BrandRepository = RepositoriesFactory.BrandRepository,
                ColourRepository = RepositoriesFactory.ColourRepository,
                CategoryRepository = RepositoriesFactory.CategoryRepository,
            };
            PurchaseService = new PurchaseService(RepositoriesFactory.PurchaseRepository);
            UserService = new UserService(
                RepositoriesFactory.UserRepository, 
                RepositoriesFactory.RoleRepository
            );
            BrandService = new BrandService(RepositoriesFactory.BrandRepository);
            ColourService = new ColourService(RepositoriesFactory.ColourRepository);
            CategoryService = new CategoryService(RepositoriesFactory.CategoryRepository);

        }
    }
}