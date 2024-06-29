using Beauty.Shared.DTOs.AppointmentType;
using Beauty.Shared.DTOs.Base;
using Beauty.Shared.DTOs.Discount;
using Beauty.Shared.DTOs.Employee;
using Beauty.Shared.DTOs.Product;
using Beauty.Shared.DTOs.Room;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Appointment
{
    public class AppointmentDto : BaseEntityDto
    {
        public bool IsSelected { get; set; }

        public string? Date { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public int RoomId { get; set; }
        public RoomDto? Room { get; set; }

        public int AppointmentTypeId { get; set; }
        public AppointmentTypeDto? AppointmentType { get; set; }

        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }

        public int? DiscountId { get; set; }
        public DiscountDto? Discount { get; set; }
    }
}
