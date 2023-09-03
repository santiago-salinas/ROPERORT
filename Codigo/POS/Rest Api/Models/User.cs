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
        public string Adress { 
            get => _adress; 
            set {
                ValidateAdress(value);
                _adress = value;
            } 
        }
        public Role Role { get; set; }

        private string _mail;
        private string _adress;

        private void ValidateMail(string value)
        {
            if (MailHasInvalidFormat(value))
                throw new Exception("Mail adress is not valid");
        }

        private bool MailHasInvalidFormat(string value)
        {
            return !(new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")).IsMatch(value);
        }

        private void ValidateAdress(string value)
        {
            if (AdressIsNull(value))
                throw new Exception("Adress is not valid");
        }

        private bool AdressIsNull(string value)
        {
            return value.Equals("");
        }
    }
}