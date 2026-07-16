using SSMS.API.Config;
using SSMS.API.Middlewares;
using SSMS.Application.DIConfig;
using SSMS.Infrastructure.DatabaseConfig;

namespace SSMS.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddInfrustructure(builder.Configuration)
                .AddApplication()
                .AddAPI(builder.Configuration);

            builder.Services.AddHttpContextAccessor(); // ho tro viec doc claim type

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                await app.Services.MigrateAndSeedAsync();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseApi();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
