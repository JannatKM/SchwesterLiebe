using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
