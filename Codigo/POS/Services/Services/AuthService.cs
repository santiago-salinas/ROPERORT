using Services.Interfaces;
using Services.Models;

namespace Services;

public class AuthService
{
    private readonly ICRUDRepository<User> _repository;

    public AuthService(ICRUDRepository<User> repo)
    {
        _repository = repo;
    }

    public string LogIn(User user)
    {
        List<User> users = _repository.GetAll();
        User u = users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
        if (u == null) return "";
        else return u.Token;
    }

    public bool IsLogged(string token)
    {
        List<User> users = _repository.GetAll();
        return users.Exists(x => x.Token == token);
    }
}