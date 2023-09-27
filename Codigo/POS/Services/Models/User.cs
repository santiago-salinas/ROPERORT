using System.Text.RegularExpressions;

namespace Services.Models
{
    public class User
    {
        public User(int id, string email, string address)
        {
            Id = id;
            Email = email;
            Address = address;
            Roles = new List<Role>();
        }

        public User()
        {

        }

        public int Id { get; set; }
        public string Email { 
            get => _mail; 
            set {
                ValidateMail(value);
                _mail = value;
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                ValidateNotNull(value);
                _password = value;
            }
        }
        public string Token
        {
            get => _token;
            set
            {
                ValidateNotNull(value);
                _token = value;
            }
        }
        public string Address { 
            get => _Address; 
            set {
                ValidateNotNull(value);
                _Address = value;
            } 
        }
        public List<Role> Roles { get; }

        public void AddRole(Role role)
        {
            if(Roles.Contains(role)) return;
            Roles.Add(role);
        }

        public void RevokeRole(Role role)
        {
            Roles.Remove(role);
        }

        private string _mail;
        private string _password;
        private string _token;
        private string _Address;

        private void ValidateMail(string value)
        {
            if (MailHasInvalidFormat(value))
                throw new Exception("Mail Address is not valid");
        }

        private bool MailHasInvalidFormat(string value)
        {
            return !(new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")).IsMatch(value);
        }

        private void ValidateNotNull(string value)
        {
            if (StringIsNull(value))
                throw new Exception("Info is not valid");
        }

        private bool StringIsNull(string value)
        {
            if (value is null) return true;
            return value.Equals("");
        }
    }
}