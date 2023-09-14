using DataAccess.Entities;
using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessInterfaces;
using System.Drawing;
using DataAccess.DatabaseServices;

namespace EntityFrameworkTests
{
    [TestClass]
    public class EFProductRepositoryTests
    {
        private IProductRepository _productRepository;
        private EFBrandService _ebrandService;
        private EFCategoryService _categoryService;
        private EFColourService _colourService;

        [TestInitialize]
        public void TestInitialize()
        {
            _productRepository = new EFProductRepository();

            
        }

        [TestCleanup]
        public void TestCleanup()
        {
            using (EFContext dbContext = new EFContext())
            {
                UserEntity user = dbContext.UserEntities.FirstOrDefault(m => m.Username == "TestUser");
                List<MaterialEntity> materials = dbContext.MaterialEntities
                    .Include(m => m.Metallic)
                    .Where(m => m.OwnerId == user.Username && m.Name.StartsWith("Test Material"))
                    .ToList();

                dbContext.MetallicEntities.RemoveRange(materials.Select(m => m.Metallic));
                dbContext.MaterialEntities.RemoveRange(materials);
                dbContext.UserEntities.Remove(user);
                dbContext.SaveChanges();
            }
        }

        [TestMethod]
        public void AddMaterial_ValidMetallic_MetallicAdded()
        {
            Metallic metallic = new Metallic
            {
                Name = "Test Material",
                Owner = "TestUser",
                Color = new Color(1, 0, 0),
                Roughness = 0.5,
            };

            _productRepository.AddMaterial(metallic);

            List<Material> materialsFromUser = _productRepository.GetMaterialsFromUser("TestUser");

            Assert.AreEqual(1, materialsFromUser.Count);
            Assert.IsInstanceOfType(materialsFromUser[0], typeof(Metallic));
            Assert.AreEqual("Test Material", materialsFromUser[0].Name);
            Assert.AreEqual("TestUser", materialsFromUser[0].Owner);
        }

        [TestMethod]
        public void GetMaterial_ExistingMetallic_ReturnsMetallic()
        {
            Metallic metallic = new Metallic
            {
                Name = "Test Material",
                Owner = "TestUser",
                Color = new Color(1, 0, 0),
                Roughness = 0.5
            };

            using (EFContext dbContext = new EFContext())
            {
                MaterialEntity materialEntity = MaterialEntity.FromDomain(metallic, dbContext);
                dbContext.MaterialEntities.Add(materialEntity);
                dbContext.SaveChanges();
            }

            Material retrievedMaterial = _productRepository.GetMaterial("Test Material", "TestUser");

            Assert.IsNotNull(retrievedMaterial);
            Assert.IsInstanceOfType(retrievedMaterial, typeof(Metallic));
            Assert.AreEqual("Test Material", retrievedMaterial.Name);
            Assert.AreEqual("TestUser", retrievedMaterial.Owner);
        }

        [TestMethod]
        public void RemoveMaterial_ExistingMetallic_MetallicRemoved()
        {
            Metallic metallic = new Metallic
            {
                Name = "Test Material",
                Owner = "TestUser",
                Color = new Color(1, 0, 0),
                Roughness = 0.5
            };

            using (EFContext dbContext = new EFContext())
            {
                MaterialEntity materialEntity = MaterialEntity.FromDomain(metallic, dbContext);
                dbContext.MaterialEntities.Add(materialEntity);
                dbContext.SaveChanges();
            }

            _productRepository.RemoveMaterial("Test Material", "TestUser");

            using (EFContext dbContext = new EFContext())
            {
                List<MaterialEntity> materials = dbContext.MaterialEntities
                    .Where(m => m.OwnerId == "TestUser")
                    .ToList();

                Assert.AreEqual(0, materials.Count);
            }
        }

        [TestMethod]
        public void ContainsMaterial_Test()
        {
            Metallic metallic = new Metallic
            {
                Name = "Test Material",
                Owner = "TestUser",
                Color = new Color(1, 0, 0),
                Roughness = 0.5
            };

            Metallic metallic2 = new Metallic
            {
                Name = "Test Material2",
                Owner = "TestUser",
                Color = new Color(0, 0.2, 0),
                Roughness = 0.8
            };

            using (EFContext dbContext = new EFContext())
            {
                MaterialEntity materialEntity = MaterialEntity.FromDomain(metallic, dbContext);
                dbContext.MaterialEntities.Add(materialEntity);
                dbContext.SaveChanges();
            }

            bool containsMaterial = _productRepository.ContainsMaterial("Test Material", "TestUser");
            bool containsMaterial2 = _productRepository.ContainsMaterial("Test Material2", "TestUser");

            Assert.IsTrue(containsMaterial);
            Assert.IsFalse(containsMaterial2);
        }
    }

}
