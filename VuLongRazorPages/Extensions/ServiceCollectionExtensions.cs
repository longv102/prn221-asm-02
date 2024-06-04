using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Repositories.Databases;
using Services;
using Services.Interfaces;

namespace VuLongRazorPages.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();

            // Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register database context
            services.AddDbContext<FunewsManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Invalid connection string!"));
            });

            // Register services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Register repositories
            services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Configure session service
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            return services;
        }
    }
}
