using Services.Models;
using System.Xml.Linq;

namespace Services.Interfaces
{
    public interface IPurchaseService
    {
        abstract public void Add(Purchase purchase);
        abstract public List<Purchase> GetAll();
        abstract public Purchase? Get(int id);
        abstract public List<Purchase> GetPurchaseHistory(string email);

    }
}
