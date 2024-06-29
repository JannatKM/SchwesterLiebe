using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.AppointmentType
{
    public class AppointmentTypeCreationDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Type { get; set; }
    }
}
