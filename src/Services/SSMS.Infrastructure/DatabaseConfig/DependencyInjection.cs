using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSMS.Application;
using SSMS.Application.Services.Authentication;
using SSMS.Application.Services.Image;
using SSMS.Application.Services.User;
using SSMS.Domain;
using SSMS.Domain.ConfigOptions;
using SSMS.Application.Repositories.Brands;
using SSMS.Application.Repositories.Categories;
using SSMS.Application.Repositories.ProductImages;
using SSMS.Application.Repositories.Products;
using SSMS.Application.Repositories.ProductSizePrices;
using SSMS.Application.Repositories.Sizes;
using SSMS.Application.Repositories.Users;
using SSMS.Infrastructure.Repositories.Brands;
using SSMS.Infrastructure.Repositories.Categories;
using SSMS.Infrastructure.Repositories.ProductImages;
using SSMS.Infrastructure.Repositories.Products;
using SSMS.Infrastructure.Repositories.ProductSizePrices;
using SSMS.Infrastructure.Repositories.Sizes;
using SSMS.Infrastructure.Repositories.Users;
using SSMS.Infrastructure.Services.Authentication;
using SSMS.Infrastructure.Services.Image;
using SSMS.Infrastructure.Services.User;

namespace SSMS.Infrastructure.DatabaseConfig
{
    public static class DependencyInjection
    {
        public static async Task MigrateAndSeedAsync(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;

            var logger = provider.GetRequiredService<ILoggerFactory>()
                .CreateLogger("Migration");

            try
            {
                var context = provider.GetRequiredService<SSMSContext>();

                await context.Database.MigrateAsync();

                await DataSeeder.SeedAsync(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Migration/Seeding error");
                throw;
            }
        }

        public static IServiceCollection AddInfrustructure(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            // database context
            services.AddDbContext<SSMSContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DB"));
            });

            //// identity
            //services.AddIdentity<User, IdentityRole>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 8;
            //})
            //    .AddEntityFrameworkStores<SSMSContext>()
            //    .AddDefaultTokenProviders();

            // initial values
            services.Configure<KeycloakSettings>(configuration.GetSection("Keycloak"));

            // cloudinary
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));

            // repositories and unit of work
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IProductSizePriceRepository, ProductSizePriceRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // dang ky dich vụ xac thuc
            services.AddHttpClient<IAuthenticationService, KeycloakAuthenticationService>();
            services.AddHttpClient<IKeycloakAdminClientService, KeycloakAdminClientService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            // services
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}