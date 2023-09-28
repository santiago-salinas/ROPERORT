using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthorizationService
    {
        private readonly ICRUDRepository<User> _repo;

        public AuthorizationService(ICRUDRepository<User> repo)
        {
            _repo = repo;
        }

        public bool IsAdmin(string token)
        {
            User user = FindUser(token);
            if (user == null) return false;
            var roles = user.Roles.ToList();
            return roles.Contains(new Role() { Name = "Admin" });
        }

        private User? FindUser(string token)
        {
            var users = _repo.GetAll();
            return users.Where(x => x.Token == token).FirstOrDefault();
        }
    }
}