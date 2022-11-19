using FantasyHelper.Shared.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FantasyHelper.API
{
    public static class Setup
    {
        public static IServiceCollection AddFantasyAPI(this IServiceCollection services, string version, string title)
        {
            //services.AddFantasyServices();
            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                });
            });

            return services;
        }

        public static void UseFantasyAPI(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                var swaggerOptions = new SwaggerOptions();
                app.Configuration.GetSection(SwaggerOptions.Key).Bind(swaggerOptions);

                app.UseSwagger(options =>
                {
                    if (String.IsNullOrEmpty(swaggerOptions.JsonRoute)) throw new NullReferenceException("Swagger JsonRoute is not set in appsettings.");
                    options.RouteTemplate = swaggerOptions.JsonRoute;
                });

                app.UseSwaggerUI(options =>
                {
                    if (String.IsNullOrEmpty(swaggerOptions.UIEndpoint)) throw new NullReferenceException("Swagger UIEndpoint is not set in appsettings.");
                    if (String.IsNullOrEmpty(swaggerOptions.Description)) throw new NullReferenceException("Swagger Description is not set in appsettings.");

                    options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
                });
            }

            app.MapControllers();
        }
    }
}
