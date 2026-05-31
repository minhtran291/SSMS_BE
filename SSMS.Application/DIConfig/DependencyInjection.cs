using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SSMS.Application.Automapper;
using SSMS.Application.Services.Image;
using SSMS.Application.Services.Product;

namespace SSMS.Application.DIConfig
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ApplicationMapper>();
            });

            //services.AddAutoMapper(cfg =>
            //{
            //    cfg.AddMaps(typeof(ApplicationMapper).Assembly);
            //});

            // Add Validator
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            // Add Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
