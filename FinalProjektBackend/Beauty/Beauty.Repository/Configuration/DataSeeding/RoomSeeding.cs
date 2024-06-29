using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.DataSeeding
{
    public class RoomSeeding : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasData(
                new Room() { Id = 1, Name = "Room 1", IsDown = false },
                new Room() { Id = 2, Name = "Room 2", IsDown = false },
                new Room() { Id = 3, Name = "Room 3", IsDown = true, Description = "Repairing" }
            );
        }
    }
}
