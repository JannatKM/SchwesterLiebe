using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class EmployeeCalendarConfiguration : IEntityTypeConfiguration<EmployeeCalendar>
    {
        public void Configure(EntityTypeBuilder<EmployeeCalendar> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsVacation)
                   .IsRequired();

            builder.Property(e => e.Date)
                   .HasMaxLength(10)
                   .IsRequired();

            builder.Property(e => e.EmployeeId)
                   .IsRequired();

            builder.Property(e => e.VacationDescription)
                   .HasMaxLength(255);
        }
    }
}
