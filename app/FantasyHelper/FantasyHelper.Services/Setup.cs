namespace FantasyHelper.Services
{
    public static class Setup
    {
        public static IServiceCollection AddFantasyServices(this IServiceCollection services)
        {

            services.AddHttpClient<IDataService, FPLDataService>("FPL");
            services.AddHostedService<FPLDataService>();

            services.AddTransient<IPlayersService, FPLPlayersService>();
            services.AddTransient<ITeamsService, FPLTeamsService>();
            services.AddTransient<ILeagueService, FPLLeagueService>();
            services.AddTransient<IEmailService, FPLEmailService>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var dailyKey = new JobKey("daily");
                q.AddJob<SendDailyNotifications>(dailyKey, job => job.StoreDurably());
                q.AddTrigger(t => t
                    .WithIdentity("Daily Trigger")
                    .ForJob(dailyKey)
                    .StartAt(new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 20, 30, 0, DateTimeKind.Utc))
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromHours(24))
                    .RepeatForever()
                    .WithMisfireHandlingInstructionFireNow())); ;
                    //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(21, 30)
                    //    .InTimeZone(TimeZoneInfo.Utc)
                    //    .WithMisfireHandlingInstructionIgnoreMisfires()).);

            });
            services.AddTransient<SendDailyNotifications>();
            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
            });

            services.AddFantasyData();
            return services;
        }
    }
}
