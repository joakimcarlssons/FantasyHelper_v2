using FantasyHelper.Data.FPL;
using FantasyHelper.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyHelper.Data
{
    public static class Setup
    {
        public static IServiceCollection AddFantasyData(this IServiceCollection services)
        {
            services.AddScoped<IRepository, FPLRepository>();
            services.AddDbContext<FPLDataContext>(opt => opt.UseInMemoryDatabase("InMemDb"));

            services.AddFantasyMappings();
            return services;
        }
    }
}
