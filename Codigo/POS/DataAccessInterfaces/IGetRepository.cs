using Rest_Api.Models;
using System.Xml.Linq;

namespace DataAccessInterfaces
{
    public interface IGetRepository<T>
    {
        abstract public List<T> GetAll();
        abstract public T? Get(string name);
    }
}
