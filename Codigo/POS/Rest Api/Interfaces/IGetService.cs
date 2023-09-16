using Models;
using System.Xml.Linq;

namespace Rest_Api.Interfaces
{
    public interface IGetService<T>
    {

        abstract public List<T> GetAll();

        abstract public T? Get(string name);
    }
}
