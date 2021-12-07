namespace JGP.NoteMaster.Api.Application.Configuration
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Class EnsureMigration.
    /// </summary>
    public static class EnsureMigration
    {
        /// <summary>
        ///     Ensures the migration of context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="app">The application.</param>
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<T>();
            context.Database.Migrate();
        }
    }
}