using Beauty.Shared.DTOs.Base;

namespace Beauty.Shared.DTOs.User
{
    public class UserDto : BaseEntityDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Telephone { get; set; }

        public string? Password { get; set; }

        public int RoleId { get; set; }
    }
}
