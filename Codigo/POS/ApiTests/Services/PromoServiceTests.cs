using Services;
using Services.Models;
using Services.Interfaces;

namespace ApiTests.Services
{
    [TestClass]
    public class PromoServiceTests
    {
        [TestMethod]
        public void GetAll_ReturnsListOfPromos()
        {
            var promoService = new PromoService();

            var result = promoService.GetAll();
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<IPromo>));
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void Get_ValidPromoName_ReturnsPromo()
        {
            var promoService = new PromoService();
            promoService.GetAll();
            var validPromoName = "3X1 Fidelity";

            var result = promoService.Get(validPromoName);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IPromo));
            Assert.AreEqual(validPromoName, result.Name);
        }

        [TestMethod]
        public void Get_InvalidPromoName_ReturnsNull()
        {
            var promoService = new PromoService();
            var invalidPromoName = "Nonexistent Promo";

            var result = promoService.Get(invalidPromoName);

            Assert.IsNull(result);
        }
    }
}
