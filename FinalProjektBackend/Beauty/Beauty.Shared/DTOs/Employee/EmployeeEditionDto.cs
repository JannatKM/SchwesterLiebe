using Beauty.Shared.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Employee
{
    public class EmployeeEditionDto : BaseEntityDto
    {
        [Required]
        public int UserId { get; set; }
    }
}
