using System.Text.RegularExpressions;

namespace Services.Models
{
    public class User
    {
        public User(int id, string email, string address, string password)
        {
            Id = id;
            Email = email;
            Address = address;
            Password = password;
            Roles = new List<Role>();
        }

        public User()
        {

        }

        public int Id { get; set; }
        public string Email {
            get { if (_mail is not null) { return _mail; } else return ""; }
            set {
                ValidateMail(value);
                _mail = value;
            }
        }
        public string Password
        {
            get { if (_password is not null) { return _password; } else return ""; }
            set
            {
                ValidateNotNull(value);
                _password = value;
            }
        }
        public string? Token
        {
            get { if (_token is not null) { return _token; } else return ""; }
            set
            {
                ValidateNotNull(value);
                _token = value;
            }
        }
        public string Address { 
            get { if (_address is not null) { return _address; } else return ""; }
            set {
                ValidateNotNull(value);
                _address = value;
            } 
        }
        public List<Role>? Roles { get; }

        public void AddRole(Role role)
        {
            if(Roles is not null)
            {
                if (Roles.Contains(role)) return;
                Roles.Add(role);
            }
            return;
        }

        public void RevokeRole(Role role)
        {
            if (Roles is not null)
            {
                Roles.Remove(role);
            }
            return;
        }

        private string? _mail;
        private string? _password;
        private string? _token;
        private string? _address;

        private void ValidateMail(string value)
        {
            if (MailHasInvalidFormat(value))
                throw new Exception("Mail Address is not valid");
        }

        private bool MailHasInvalidFormat(string value)
        {
            return !(new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")).IsMatch(value);
        }

        private void ValidateNotNull(string? value)
        {
            if (StringIsNull(value))
                throw new Exception("Info is not valid");
        }

        private bool StringIsNull(string? value)
        {
            if (value is null) return true;
            return value.Equals("");
        }
    }
}