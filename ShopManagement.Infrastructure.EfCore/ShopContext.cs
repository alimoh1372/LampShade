using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore.Mapping;
using SM.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore
{
    public class ShopContext:DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }

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