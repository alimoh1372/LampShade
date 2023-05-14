using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            builder.OwnsMany(x => x.Permissions, modelBuilder =>
            {
                modelBuilder.ToTable("RolePermissions");
                modelBuilder.HasKey(x => x.Id);
                modelBuilder.Ignore(x => x.Name);
                modelBuilder.WithOwner(x => x.Role);
            });
        }
    }
}