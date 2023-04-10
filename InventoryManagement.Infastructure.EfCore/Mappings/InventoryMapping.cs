using System.Security.Cryptography.X509Certificates;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EfCore.Mappings
{
    public class InventoryMapping:IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(x => x.Id);

            builder.OwnsMany(x => x.InventoryOperations, bodybuilder =>
            {
                bodybuilder.ToTable("InventoryOperations");
                bodybuilder.HasKey(x => x.Id);
                bodybuilder.Property(x => x.Description).HasMaxLength(1000);
                bodybuilder.WithOwner(x => x.Inventory)
                    .HasForeignKey(x => x.FkInventoryId);
            });
                
           
        }
    }
}