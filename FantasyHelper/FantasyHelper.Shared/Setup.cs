using Microsoft.Extensions.DependencyInjection;

namespace FantasyHelper.Shared
{
    public static class Setup
    {
        public static IServiceCollection AddFantasyMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
