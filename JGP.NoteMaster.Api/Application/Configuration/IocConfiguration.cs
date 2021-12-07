namespace JGP.NoteMaster.Api.Application.Configuration
{
    using System;
    using Core.Configuration;
    using Data.EntityFramework;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    internal static class IocConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            configuration.GetSection(AppSettings.ConfigurationSectionName).Bind(appSettings);
            services.AddSingleton(appSettings);

            services.AddSingleton(configuration);

            var noteConnectionString = configuration.GetConnectionString("NoteContext");
            services.AddDbContext<NoteContext>(options => options.UseSqlServer(noteConnectionString,
                x => x.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null)));

            services.AddTransient<INoteContext, NoteContext>();
            services.AddTransient<INoteService, NoteService>();
        }
    }
}