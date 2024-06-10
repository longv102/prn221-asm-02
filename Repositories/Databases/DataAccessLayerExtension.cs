using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories.Databases
{
    public static class DataAccessLayerExtension
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FunewsManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Invalid connection string"));
            });

            return services;
        }
    }
}
