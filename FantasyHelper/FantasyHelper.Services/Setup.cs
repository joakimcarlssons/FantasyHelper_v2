using FantasyHelper.Data;
using FantasyHelper.Services.FPL;
using FantasyHelper.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyHelper.Services
{
    public static class Setup
    {
        public static IServiceCollection AddFantasyServices(this IServiceCollection services)
        {

            services.AddHttpClient<IDataService, FPLDataService>("FPL");
            services.AddHostedService<FPLDataService>();

            services.AddTransient<IPlayersService, FPLPlayersService>();
            services.AddTransient<ILeagueService, FPLLeagueService>();
            services.AddFantasyData();
            return services;
        }
    }
}
