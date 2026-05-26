using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SSMS.Domain.ConfigOptions;
using SSMS.Domain.Entities;
using SSMS.Persistence.UnitOfWork;
using SSMS.Persistence.Repositories.Product;

namespace SSMS.Persistence.DatabaseConfig
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
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = provider.GetRequiredService<UserManager<User>>();
                var seedOptions = provider.GetRequiredService<IOptions<SeedSettings>>();

                await context.Database.MigrateAsync();

                await DataSeeder.SeedAsync(
                    context,
                    roleManager,
                    userManager,
                    seedOptions
                );
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

            // identity
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<SSMSContext>()
                .AddDefaultTokenProviders();

            // initial values
            var seedSettingsSection = configuration.GetSection("SeedSettings");
            services.Configure<SeedSettings>(seedSettingsSection);

            // repositories and unit of work
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}