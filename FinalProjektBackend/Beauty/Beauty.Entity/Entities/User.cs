using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Telephone { get; set; }

        public string? Password { get; set; }

        public int RoleId { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
