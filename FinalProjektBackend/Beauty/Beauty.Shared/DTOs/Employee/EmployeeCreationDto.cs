using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Employee
{
    public class EmployeeCreationDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
