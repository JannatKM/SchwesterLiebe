using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsSelected)
                   .IsRequired();

            builder.Property(e => e.Date)
                   .IsRequired();

            builder.Property(e => e.StartTime)
                   .IsRequired();

            builder.Property(e => e.EndTime)
                   .IsRequired();

            builder.Property(e => e.EmployeeId)
                   .IsRequired();

            builder.Property(e => e.RoomId)
                   .IsRequired();

            builder.Property(e => e.AppointmentTypeId)
                   .IsRequired();

            //builder.HasMany(e => e.Bookings)
            //       .WithOne(x => x.Appointment)
            //       .HasForeignKey(x => x.AppointmentId)
            //       .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
