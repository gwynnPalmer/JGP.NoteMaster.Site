namespace JGP.NoteMaster.Site.Application.Configuration
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Web.Proxy;

    /// <summary>
    ///     Class IocConfiguration.
    /// </summary>
    public static class IocConfiguration
    {
        /// <summary>
        ///     Configures the specified services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="appSettings">The application settings.</param>
        public static void Configure(IServiceCollection services, IConfiguration configuration, AppSettings appSettings)
        {
            services.AddSingleton(configuration);
            services.AddSingleton(appSettings);

            services.AddMemoryCache(options => { options.ExpirationScanFrequency = TimeSpan.FromSeconds(15); });

            //Allow claims principle injection
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var apiHelper = new NoteApiHelper(appSettings.ApiEndpoint);
            services.AddSingleton<INoteApiHelper>(apiHelper);
        }
    }
}