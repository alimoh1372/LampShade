﻿using InventoryManagement.Domain.InventoryAgg;
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

            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));

        }
    }
}