using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Date)
                   .IsRequired();

            builder.Property(e => e.Time)
                   .IsRequired();

            //builder.Property(e => e.AppointmentId)
            //       .IsRequired();

            builder.Property(e => e.EmployeeId)
                   .IsRequired();

            builder.Property(e => e.CustomerId)
                   .IsRequired();

            builder.Property(e => e.ProductId)
                   .IsRequired();

            builder.Property(e => e.RoomId)
                   .IsRequired();
        }
    }
}
