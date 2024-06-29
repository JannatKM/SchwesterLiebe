using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class Appointment : BaseEntity
    {
        public bool IsSelected { get; set; }

        public string? Date { get; set; }

        public string? StartTime { get; set; }

        public string? EndTime { get; set; }

        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public int AppointmentTypeId { get; set; }
        public AppointmentType? AppointmentType { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
