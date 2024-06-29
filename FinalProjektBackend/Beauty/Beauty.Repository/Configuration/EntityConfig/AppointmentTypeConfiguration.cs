using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class AppointmentTypeConfiguration : IEntityTypeConfiguration<AppointmentType>
    {
        public void Configure(EntityTypeBuilder<AppointmentType> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Type)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasMany(e => e.Appointments)
                   .WithOne(x => x.AppointmentType)
                   .HasForeignKey(x => x.AppointmentTypeId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
