using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(400);
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(400);




            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductPictures)
                .HasForeignKey(x => x.FkProductId);




        }
    }
}