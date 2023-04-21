using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EfCore.Mapping
{
    public class ArticleCategoryMapping:IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {
            builder.ToTable("ArticleCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).HasMaxLength(300);
            builder.Property(x => x.PictureTitle).HasMaxLength(400);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.ShowOrder);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Keywords).IsRequired().HasMaxLength(80);
            builder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(150);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(1000);

        }
    }
}