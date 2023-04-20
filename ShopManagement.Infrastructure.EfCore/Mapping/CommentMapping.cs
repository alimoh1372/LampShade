using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class CommentMapping:IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Message).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Point).IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.FkProductId);

        }
    }
}