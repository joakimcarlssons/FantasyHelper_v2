using FantasyHelper.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyHelper.Services
{
    public static class Setup
    {
        public static IServiceCollection AddFantasyServices(this IServiceCollection services)
        {
            services.AddFantasyMappings();
            return services;
        }
    }
}
