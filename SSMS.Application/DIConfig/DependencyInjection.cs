using Microsoft.Extensions.DependencyInjection;
using SSMS.Application.Automapper;
using SSMS.Application.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Application.DIConfig
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(typeof(ApplicationMapper));

            // Add Services
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
