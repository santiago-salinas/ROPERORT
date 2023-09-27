using Services.Models;


namespace DataAccess.Entities
{
    public class AssignedRoles
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public string RoleName { get; set; }
        public RoleEntity Role { get; set; }

        public AssignedRoles() { }

        public static AssignedRoles FromModel(User user, Role role)
        {
            return new AssignedRoles
            {
                UserId = user.Id,
                User = UserEntity.FromModel(user),
                RoleName = role.Name,
                Role = RoleEntity.FromModel(role),
            };
        }
    }
}
