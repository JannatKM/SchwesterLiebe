using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.DataSeeding
{
    public class RoleSeeding : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role() { Id = 1, Name = "Admin" },
                new Role() { Id = 2, Name = "Customer" },
                new Role() { Id = 3, Name = "Employee" }
            );
        }
    }
}
