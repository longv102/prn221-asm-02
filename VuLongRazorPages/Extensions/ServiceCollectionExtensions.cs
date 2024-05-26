using DAL.Database;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using VuLongRazorPages.Middlewares;

namespace VuLongRazorPages.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();

            // Register database provider
            services.AddDbContext<FunewsManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string is not valid!"));
            });

            // Register middelware for handling errors and exceptions
            services.AddTransient<GlobalExceptionHandlingMiddleware>();

            // Register repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();

            // Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
