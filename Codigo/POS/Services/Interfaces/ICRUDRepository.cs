namespace Services.Interfaces
{
    public interface ICRUDRepository<T>
    {

        abstract public List<T> GetAll();

        abstract public T? Get(int id);

        abstract public void Add(T entity);

        abstract public void Delete(int id);

        abstract public void Update(T entity);
    }
}
