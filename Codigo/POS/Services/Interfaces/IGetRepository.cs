namespace Services.Interfaces
{
    public interface IGetRepository<T>
    {
        abstract public List<T> GetAll();
        abstract public T? Get(string name);
    }
}
