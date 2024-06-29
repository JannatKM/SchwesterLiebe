using Beauty.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beauty.Repository.Configuration.EntityConfig
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.StartDate)
                   .IsRequired();
            
            builder.Property(e => e.EndDate)
                   .IsRequired();

            builder.Property(e => e.Percent)
                   .IsRequired();

            builder.HasMany(e => e.Appointments)
                   .WithOne(x => x.Discount)
                   .HasForeignKey(x => x.DiscountId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            //builder.HasMany(e => e.Bookings)
            //       .WithOne(x => x.Discount)
            //       .HasForeignKey(x => x.DiscountId)
            //       .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
