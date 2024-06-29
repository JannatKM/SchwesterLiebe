using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class Employee : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<EmployeeCalendar>? EmployeeCalendars { get; set; }
    }
}
