using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class Role : BaseEntity
    {
        public string? Name { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
