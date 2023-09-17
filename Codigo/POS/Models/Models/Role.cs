namespace Models
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

        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object? obj)
        {
            return Name.Equals(obj?.ToString());
        }

        private string _name;

        private void ValidateRole(string name)
        {
            if (RoleIsNotValid(name))
                throw new Exception("Invalid role");
        }

        private bool RoleIsNotValid(string name)
        {
            return !name.Equals("Customer") && !name.Equals("Admin");
        }
    }
}