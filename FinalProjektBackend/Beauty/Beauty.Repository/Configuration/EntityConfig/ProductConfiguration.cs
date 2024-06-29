using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(e => e.Description)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(e => e.Duration)
                   .IsRequired();

            builder.Property(e => e.Price)
                   .IsRequired();

            builder.HasMany(e => e.Bookings)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.ClientCascade);



            builder.HasMany(e => e.Appointments)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
