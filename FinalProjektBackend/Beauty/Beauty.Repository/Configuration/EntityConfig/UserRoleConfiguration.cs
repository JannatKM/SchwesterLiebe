using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.HasOne<User>(e => e.User)
                   .WithMany(x => x.UserRoles)
                   .HasForeignKey(e => e.UserId);

            builder.HasOne<Role>(e => e.Role)
                   .WithMany(x => x.UserRoles)
                   .HasForeignKey(e => e.RoleId);
        }
    }
}
