using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                   .IsRequired();

            builder.HasMany(e => e.Bookings)
                   .WithOne(x => x.Employee)
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(e => e.Appointments)
                   .WithOne(x => x.Employee)
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(e => e.EmployeeCalendars)
                   .WithOne(x => x.Employee)
                   .HasForeignKey(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
