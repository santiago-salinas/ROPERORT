using Services.Exceptions;
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
            try
            {
                _repository.Add(entity);
            }
            catch (DatabaseException ex)
            {
                throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
            }
        }

        public Purchase? Get(int id)
        {
            try
            {

                return _repository.Get(id);
            }
            catch (DatabaseException ex)
            {
                throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
            }
        }

        public List<Purchase> GetAll()
        {
            try
            {
                return _repository.GetAll();

            }
            catch (DatabaseException ex)
            {
                throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
            }
        }

        public List<Purchase> GetPurchaseHistory(string email)
        {
            List<Purchase> list;
            try
            {
                list = _repository.GetAll();
            }
            catch (DatabaseException ex)
            {
                throw new Service_ObjectHandlingException("Exception catched from the repository: " + ex.Message);
            }
            List<Purchase> history = list.Where(p => p.Client.Email == email).ToList();
            return history;
        }
    }
}
