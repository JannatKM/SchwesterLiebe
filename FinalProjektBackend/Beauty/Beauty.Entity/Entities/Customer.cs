using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class Customer : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
