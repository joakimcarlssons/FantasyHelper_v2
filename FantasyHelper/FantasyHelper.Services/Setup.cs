using FantasyHelper.Services.Allsvenskan;
using FantasyHelper.Services.FPL;
using FantasyHelper.Services.Scheduled;
using Microsoft.Extensions.Configuration;
using Quartz;
using System.Net;

namespace FantasyHelper.Services
{
    public static class Setup
    {
        public delegate IPlayersService PlayersFactory(FantasyGames game);
        public delegate ILeagueService LeagueFactory(FantasyGames game);

        public static IServiceCollection AddFantasyServices(this IServiceCollection services)
        {
            services.AddHttpClient<IDataService, ASDataService>("Allsvenskan").ConfigurePrimaryHttpMessageHandler(cfg => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            services.AddHttpClient<IDataService, FPLDataService>("FPL");

            services.AddHostedService<FPLDataService>();
            services.AddHostedService<ASDataService>();

            services.AddTransient<FPLPlayersService>();
            services.AddTransient<ASPlayersService>();
            services.AddTransient<PlayersFactory>(serviceProvider => game =>
            {
                return game switch
                {
                    FantasyGames.Allsvenskan => serviceProvider.GetRequiredService<ASPlayersService>(),
                    _ => serviceProvider.GetRequiredService<FPLPlayersService>()
                };
            });

            services.AddTransient<FPLLeagueService>();
            services.AddTransient<ASLeagueService>();
            services.AddTransient<LeagueFactory>(sp => game =>
            {
                return game switch
                {
                    FantasyGames.Allsvenskan => sp.GetRequiredService<ASLeagueService>(),
                    _ => sp.GetRequiredService<FPLLeagueService>()
                };
            });

            services.AddTransient<IFixturesService, ASFixturesService>();
            services.AddScoped<IEmailService, FPLEmailService>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var dailyKey = new JobKey("daily");
                q.AddJob<SendDailyNotifications>(dailyKey, job => job.StoreDurably());
                q.AddTrigger(t => t
                    .WithIdentity("Daily Trigger")
                    .ForJob(dailyKey)
                    .StartAt(DateTime.UtcNow.AddSeconds(10))
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromHours(24)).RepeatForever()));

            });
            services.AddTransient<SendDailyNotifications>();
            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}
