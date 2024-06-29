using Beauty.Entity.Base;

namespace Beauty.Entity.Entities
{
    public class AppointmentType : BaseEntity
    {
        public string? Type { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
