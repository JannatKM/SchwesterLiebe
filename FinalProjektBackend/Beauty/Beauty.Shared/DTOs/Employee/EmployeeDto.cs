using Beauty.Shared.DTOs.Base;
using Beauty.Shared.DTOs.User;

namespace Beauty.Shared.DTOs.Employee
{
    public class EmployeeDto : BaseEntityDto
    {
        public int UserId { get; set; }
        public UserDto? User { get; set; }
    }
}
