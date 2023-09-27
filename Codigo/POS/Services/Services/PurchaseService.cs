using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ICRUDRepository<Purchase> _repository;
        public PurchaseService(ICRUDRepository<Purchase> repository) 
        {
            _repository = repository;
        }

        public void Add(Purchase entity)
        {
            _repository.Add(entity);
        }

        public Purchase? Get(int id)
        {
            return _repository.Get(id);
        }

        public List<Purchase> GetAll()
        {
            return _repository.GetAll();
        }

        public List<Purchase> GetPurchaseHistory(string email) 
        {
            List<Purchase> list = _repository.GetAll();
            List<Purchase> history = list.Where(p => p.Client.Email == email).ToList();
            return history;
        }
    }
}
