using Services.Exceptions;

namespace Services.Models
{
    public class Role
    {
        public string Name
        {
            get => _name;
            set
            {
                ValidateRole(value);
                _name = value;
            }
        }

        public Role(string name)
        {
            _name = name;
        }

        public Role()
        {}
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
                throw new Service_ArgumentException("Invalid role");
        }

        private bool RoleIsNotValid(string name)
        {
            return !name.Equals("Customer") && !name.Equals("Admin");
        }
    }
}