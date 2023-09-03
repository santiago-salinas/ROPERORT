using System.Text.RegularExpressions;

namespace Rest_Api.Models
{
    public class User
    {
        public string Mail { 
            get => _mail; 
            set {
                ValidateMail(value);
                _mail = value;
            }
        }
        public string Address { 
            get => _Address; 
            set {
                ValidateAddress(value);
                _Address = value;
            } 
        }
        public Role Role { get; set; }

        private string _mail;
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

        private void ValidateAddress(string value)
        {
            if (AddressIsNull(value))
                throw new Exception("Address is not valid");
        }

        private bool AddressIsNull(string value)
        {
            return value.Equals("");
        }
    }
}