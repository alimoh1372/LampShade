using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogManagement.Infrastructure.EfCore.Mapping
{
    public class ArticleMapping:IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(x => x.Id);


            builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ShortDescription).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(300);
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(400);
            builder.Property(x => x.Slug).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Keywords).HasMaxLength(80);
            builder.Property(x => x.MetaDescription).HasMaxLength(150);
            builder.Property(x => x.CanonicalAddress).HasMaxLength(1000);
            builder.Property(x => x.FkArticleCategoryId).IsRequired().HasMaxLength(1000);


            builder.HasOne(x => x.ArticleCategory)
                .WithMany(x => x.Articles)
                .HasForeignKey(x => x.FkArticleCategoryId);
        }
    }
}