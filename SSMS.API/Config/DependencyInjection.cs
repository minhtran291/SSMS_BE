using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SSMS.Domain.ConfigOptions;

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

            // cau hinh goi key cloak
            var jwtSettings = new JwtSettings();

            configuration.GetSection(JwtSettings.SectionName).Bind(jwtSettings);

            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = jwtSettings.Authority;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        NameClaimType = "preferred_username",
                    };
                });

            services.AddAuthorization();

            return services;
        }

        public static IApplicationBuilder UseApi(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicy);
            return app;
        }
    }
}
