using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EfCore.Mapping
{
    public class CustomerDiscountMapping : IEntityTypeConfiguration<CustomerDiscount>
    {
        public void Configure(EntityTypeBuilder<CustomerDiscount> builder)
        {
            builder.ToTable("CustomerDiscounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FkProductId).IsRequired();
            builder.Property(x => x.Reason).IsRequired().HasMaxLength(500);
            builder.Property(x => x.StartDate).IsRequired().HasMaxLength(20);
            builder.Property(x => x.EndDate).IsRequired().HasMaxLength(20);
    

        }
    }
}
