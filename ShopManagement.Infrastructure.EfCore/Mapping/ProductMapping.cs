using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();

            builder.Property(x => x.Code).HasMaxLength(15).IsRequired();

            builder.Property(x => x.ShortDescription).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(300);
            builder.Property(x => x.PictureTitle).HasMaxLength(400);
            builder.Property(x => x.Slug).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();


            builder.HasOne(x => x.ProductCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.FkCategoryId);

            builder.HasMany(x => x.ProductPictures)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.FkProductId);
        }
    }
}