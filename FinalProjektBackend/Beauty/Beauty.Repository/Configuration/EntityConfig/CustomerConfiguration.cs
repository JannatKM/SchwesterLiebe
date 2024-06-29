using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UserId)
                   .IsRequired();

            builder.HasMany(e => e.Bookings)
                   .WithOne(x => x.Customer)
                   .HasForeignKey(x => x.CustomerId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
