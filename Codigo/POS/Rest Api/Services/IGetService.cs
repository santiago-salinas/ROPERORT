using Rest_Api.Models;
using System.Xml.Linq;

namespace Rest_Api.Services
{
    public interface IGetService<T>
    {

        abstract public List<T> GetAll();

        abstract public T? Get(string name);
    }
}
