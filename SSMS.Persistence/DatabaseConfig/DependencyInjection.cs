using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSMS.Domain.ConfigOptions;
using SSMS.Domain;
using SSMS.Infrustructure.Repositories.Brands;
using SSMS.Infrustructure.Repositories.Categories;
using SSMS.Infrustructure.Repositories.Products;
using SSMS.Infrustructure.Repositories.ProductImages;
using SSMS.Infrustructure.Repositories.ProductSizePrices;
using SSMS.Infrustructure.Repositories.Sizes;
using SSMS.Domain.Repositories.Products;
using SSMS.Domain.Repositories.Categories;
using SSMS.Domain.Repositories.Brands;
using SSMS.Domain.Repositories.Sizes;
using SSMS.Domain.Repositories.ProductSizePrices;
using SSMS.Domain.Repositories.ProductImages;

namespace SSMS.Infrustructure.DatabaseConfig
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

        public static IServiceCollection AddPersistence(
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

            // repositories and unit of work
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IProductSizePriceRepository, ProductSizePriceRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}