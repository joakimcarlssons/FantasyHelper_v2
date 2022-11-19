using FantasyHelper.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyHelper.Services
{
    public static class Setup
    {
        public static IServiceCollection AddFantasyServices(this IServiceCollection services)
        {
            services.AddFantasyData();
            return services;
        }
    }
}
