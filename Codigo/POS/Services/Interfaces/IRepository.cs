using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Services.Interfaces
{
    public interface IRepository<T>
    {
        abstract public List<T> GetAll();

        abstract public T? Get(int id);

        abstract public void Add(T entity);

        abstract public void Delete(int id);

        abstract public void Update(T entity);
    }
}