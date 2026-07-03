using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SSMS.Application.Automapper;
using SSMS.Application.Behaviors;
using SSMS.Application.Services.Product;
using System.Reflection;

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

            // add MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

                // dang ky pipeline behavior de tu dong validate
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            });

            return services;
        }
    }
}
