using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore.Mapping;
using SM.Domain.ProductAgg;
using SM.Domain.ProductCategoryAgg;
using SM.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore
{
    public class ShopContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assembly = typeof(ProductCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}