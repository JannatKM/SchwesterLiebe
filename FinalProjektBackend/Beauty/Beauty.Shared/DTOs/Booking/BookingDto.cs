using Beauty.Entity.Entities;
using Beauty.Shared.DTOs.Appointment;
using Beauty.Shared.DTOs.Base;
using Beauty.Shared.DTOs.Customer;
using Beauty.Shared.DTOs.Discount;
using Beauty.Shared.DTOs.Employee;
using Beauty.Shared.DTOs.Product;
using Beauty.Shared.DTOs.Room;
using Beauty.Shared.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.DTOs.Booking
{
    public class BookingDto : BaseEntityDto
    {
        public string? Date { get; set; }

        public string? Time { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeDto? Employee { get; set; }

        public int CustomerId { get; set; }
        public CustomerDto? Customer { get; set; }

        //public int AppointmentId { get; set; }
        //public AppointmentDto? Appointment { get; set; }

        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }

        public int RoomId { get; set; }
        public RoomDto? Room { get; set; }
    }
}
