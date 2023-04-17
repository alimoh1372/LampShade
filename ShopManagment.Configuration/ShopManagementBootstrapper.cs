using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using _01_LampshadeQuery.Contracts.Slide;
using _01_LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.ProductCategoryContracts;
using ShopManagement.Application.Contracts.ProductContracts;
using ShopManagement.Application.Contracts.ProductPictureContracts;
using ShopManagement.Application.Contracts.SlideContracts;
using ShopManagement.Application.ProductApplication;
using ShopManagement.Application.ProductCategoryApplication;
using ShopManagement.Application.ProductPictureApplication;
using ShopManagement.Application.SlideApplication;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;
using SM.Domain.ProductAgg;
using SM.Domain.ProductCategoryAgg;
using SM.Domain.ProductPictureAgg;
using SM.Domain.SlideAgg;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();



            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}