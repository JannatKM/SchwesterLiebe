using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class Discount : BaseEntity
    {
        public string? StartDate { get; set; }

        public string? EndDate { get; set; }

        public int Percent { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
        //public ICollection<Booking>? Bookings { get; set; }
    }
}
