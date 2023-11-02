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


            var result = promoService.GetAvailablePromosFromDirectory();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<IPromo>));
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void Get_ValidPromoName_ReturnsPromo()
        {
            var promoService = new PromoService();
            var validPromoName = new FidelityPromo().Name;

            var result = promoService.Get(validPromoName);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Promo));
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
