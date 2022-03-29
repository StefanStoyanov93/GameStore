using GameStore.Core.Serveces;
using GameStore.Core.Serveces.Contracts;
using GameStore.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStoreServices, StoreServices>();

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, 
            IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<GameStoreDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
