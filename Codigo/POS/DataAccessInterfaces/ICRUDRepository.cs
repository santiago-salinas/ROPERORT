using Rest_Api.Models;
using System.Xml.Linq;

namespace DataAccessInterfaces
{
    public interface ICRUDRepository<T>
    {

        abstract public List<T> GetAll(Func<T,bool>? filter = null);

        abstract public T? Get(int id);

        abstract public void Add(T entity);

        abstract public void Delete(int id);

        abstract public void Update(T entity);
    }
}
