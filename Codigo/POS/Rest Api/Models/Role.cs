namespace Rest_Api.Models
{
    public class Role
    {
        public string Name { 
            get => _name; 
            set {
                ValidateRole(value);
                _name = value;
            } 
        }

        private string _name;

        private void ValidateRole(string name)
        {
            if (RoleIsNotValid(name))
                throw new Exception("Invalid role");
        }

        private bool RoleIsNotValid(string name)
        {
            return !name.Equals("Customer") && !name.Equals("Admin") && !name.Equals("Both");
        }
    }
}