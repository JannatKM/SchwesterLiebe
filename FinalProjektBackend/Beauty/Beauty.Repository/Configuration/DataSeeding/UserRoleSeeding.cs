using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.DataSeeding
{
    public class UserRoleSeeding : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                new UserRole()
                {
                    UserId = 1,
                    RoleId = 1,
                }
            );
        }
    }
}
