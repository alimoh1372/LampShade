using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SM.Domain.SlideAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class SlideMapping:IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slides");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(300);
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Heading).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(400);
            builder.Property(x => x.Text).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(50);

           
          

          
            

       
        }
    }
}