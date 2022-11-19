using FantasyHelper.Data.Allsvenskan;
using FantasyHelper.Data.FPL;
using FantasyHelper.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyHelper.Data
{
    public static class Setup
    {
        public delegate IRepository DbFactory(FantasyGames game);

        public static IServiceCollection AddFantasyData(this IServiceCollection services)
        {
            services.AddScoped<FPLRepository>();
            services.AddScoped<ASRepository>();
            services.AddScoped<DbFactory>(serviceProvider => game =>
            {
                return game switch
                {
                    FantasyGames.Allsvenskan => serviceProvider.GetRequiredService<ASRepository>(),
                    _ => serviceProvider.GetRequiredService<FPLRepository>()
                };
            });

            services.AddDbContext<FPLDataContext>(opt => opt.UseInMemoryDatabase("InMemDb"));
            services.AddDbContext<ASDataContext>(opt => opt.UseInMemoryDatabase("InMemDb"));

            return services;
        }
    }
}
