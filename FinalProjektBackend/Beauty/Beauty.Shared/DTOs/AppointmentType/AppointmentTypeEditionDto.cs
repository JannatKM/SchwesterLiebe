using Beauty.Shared.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.AppointmentType
{
    public class AppointmentTypeEditionDto : BaseEntityDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Type { get; set; }
    }
}
