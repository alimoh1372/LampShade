using _0_Framework.Infrastructure;
using InventoryManagement.Application;
using InventoryManagement.ApplicationContracts.InventoryContracts;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using InventoryManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();


            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));

        }
    }
}