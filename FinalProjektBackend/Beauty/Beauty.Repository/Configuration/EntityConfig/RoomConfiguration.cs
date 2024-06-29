using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.IsDown)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasMaxLength(250);

            builder.HasMany(e => e.Bookings)
                   .WithOne(x => x.Room)
                   .HasForeignKey(x => x.RoomId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(e => e.Appointments)
                   .WithOne(x => x.Room)
                   .HasForeignKey(x => x.RoomId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
