using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.DataSeeding
{
    public class ProductSeeding : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Hair Care",
                    Duration = 60,
                    Price = 10,
                    Description = "Hair Care Service",
                },
                new Product()
                {
                    Id = 2,
                    Name = "Facial",
                    Duration = 60,
                    Price = 19,
                    Description = "Facial Service",
                },
                new Product()
                {
                    Id = 3,
                    Name = "Massages",
                    Duration = 120,
                    Price = 49,
                    Description = "Massages Service",
                },
                new Product()
                {
                    Id = 4,
                    Name = "Waxing",
                    Duration = 180,
                    Price = 99,
                    Description = "Waxing Service",
                },
                new Product()
                {
                    Id = 5,
                    Name = "Nail",
                    Duration = 60,
                    Price = 29,
                    Description = "Nail Service",
                }
            );
        }
    }
}
