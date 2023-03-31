using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductCategoryMapping:IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(x => x.Picture).
                HasMaxLength(1000)
                .IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.PictureAlt).HasMaxLength(255);
            builder.Property(x => x.PictureTitle).HasMaxLength(500);
            builder.Property(x => x.Keywords).HasMaxLength(80).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(255).IsRequired();



            builder.HasMany(x => x.Products)
                .WithOne(x => x.ProductCategory)
                .HasForeignKey(x => x.FkCategoryId);

        }
    }
}