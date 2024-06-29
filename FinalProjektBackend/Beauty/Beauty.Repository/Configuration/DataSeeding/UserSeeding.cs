using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.DataSeeding
{
    public class UserSeeding : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = 1,
                    Email = "admin@gmail.com",
                    FirstName = "John",
                    LastName = "Smith",
                    Password = "123456",
                    RoleId = 1,
                    Telephone = "12345678901"
                }
            );
        }
    }
}
