using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Email)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Telephone)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Password)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasMany(e => e.UserRoles)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
