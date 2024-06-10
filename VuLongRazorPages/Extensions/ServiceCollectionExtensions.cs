using Microsoft.AspNetCore.Authentication.Cookies;
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

            // Using session
            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
            {
                options.LoginPath = "/Auth/Signin";
            });

            // Register AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Register DbContext
            services.AddDatabaseContext(configuration);

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
