using Services.Models;
using System.Xml.Linq;

namespace Services.Interfaces
{
    public interface IGetService<T>
    {

        abstract public List<T> GetAll();

        abstract public T? Get(string name);
    }
}
