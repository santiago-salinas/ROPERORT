using System.Text.RegularExpressions;

namespace Services.Models
{
    public class User
    {
        private string? _mail;
        private string? _password;
        private string? _token;
        private string? _address;

        public User(string email, string address, string password, List<Role>? roles = null)
        {
            Email = email;
            Address = address;
            Password = password;
            Roles = roles ?? new List<Role>();
            Token = $"token{Email}secure";
        }

        public User()
        {
            Roles = new List<Role>();
        }

        public int Id { get; set; }
        public string Email
        {
            get { if (_mail is not null) { return _mail; } else return ""; }
            set
            {
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
                _token = value;
            }
        }
        public string Address
        {
            get { if (_address is not null) { return _address; } else return ""; }
            set
            {
                ValidateNotNull(value);
                _address = value;
            }
        }
        public List<Role>? Roles { set; get; }

        public void GenerateToken()
        {
            Token = $"token{Email}secure";
        }

        public void AddRole(Role role)
        {
            if (Roles is not null)
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

        public override bool Equals(object obj)
        {
            User otherUser = (User)obj;

            return Email == otherUser.Email &&
                   Address == otherUser.Address &&
                   Password == otherUser.Password;
        }
        

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