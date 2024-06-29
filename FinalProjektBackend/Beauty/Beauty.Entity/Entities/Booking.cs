using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class Booking : BaseEntity
    {
        public string? Date { get; set; }

        public string? Time { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        //public int AppointmentId { get; set; }
        //public Appointment? Appointment { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int  RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
