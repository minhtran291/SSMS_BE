namespace SSMS.API.Config
{
    public static class DependencyInjection
    {
        private const string CorsPolicy = "Frontend";
        public static IServiceCollection AddAPI(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

            services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                // tat tu dong bao required cho dto de quan ly het o fluent validation
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy, 
                    policy =>
                    {
                        policy
                            .WithOrigins(origins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                            //.AllowCredentials();
                    });
            });

            return services;
        }

        public static IApplicationBuilder UseApi(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicy);
            return app;
        }
    }
}
