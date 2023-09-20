using Services.Models;

namespace Services.Interfaces
{
    public interface IUserService
    {
        public void Add(User entity);

        public void Delete(int id);

        public User? Get(int id);

        public List<User> GetAll();

        public void Update(User entity);
    }
}
