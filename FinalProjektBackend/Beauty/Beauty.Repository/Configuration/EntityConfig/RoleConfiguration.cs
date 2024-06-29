using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasMany(e => e.UserRoles)
                   .WithOne(x => x.Role)
                   .HasForeignKey(x => x.RoleId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
