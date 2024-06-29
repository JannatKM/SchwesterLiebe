using Beauty.Shared.DTOs.Base;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Appointment
{
    public class AppointmentEditionDto : BaseEntityDto
    {
        [Required]
        public bool IsSelected { get; set; }

        [Required]
        public string? Date { get; set; }


        [Required]
        public string? StartTime { get; set; }


        [Required]
        public string? EndTime { get; set; }


        [Required]
        public int EmployeeId { get; set; }


        [Required]
        public int RoomId { get; set; }


        [Required]
        public int AppointmentTypeId { get; set; }


        [Required]
        public int ProductId { get; set; }


        public int? DiscountId { get; set; }
    }
}
