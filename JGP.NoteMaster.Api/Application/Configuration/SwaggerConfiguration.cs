namespace JGP.NoteMaster.Api.Application.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    ///     Class SwaggerConfiguration.
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        ///     Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "JGP NoteMaster", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.UseInlineDefinitionsForEnums();
                options.CustomSchemaIds(SchemaIdStrategy);


                //options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.ApiKey,
                //    In = ParameterLocation.Header,
                //    Name = "X-Api-Key",
                //    Description = "Api Key Authentication"
                //});
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                //        },
                //        new string[] { }
                //    }
                //});

                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                options.DocumentFilter<RemovePagedDataFilter>();

                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out var methodInfo))
                        return false;

                    var versions = methodInfo
                        .DeclaringType?
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = methodInfo
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToList();

                    return versions?.Any(v => $"v{v}" == version) == true
                           && (!maps.Any() || maps.Any(v => $"v{v}" == version));
                });
            });
        }

        /// <summary>
        ///     Configures the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public static void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JGP NoteMaster V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseSwagger(x =>
                {
                    x.RouteTemplate = "/{documentName}/swagger.json";
                    x.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    {
                        swaggerDoc.Servers = env.IsProduction()
                            ? new List<OpenApiServer>
                                { new() { Url = $"{httpReq.Scheme}://api.jgp.com/notemaster" } }
                            : new List<OpenApiServer>
                            {
                                new() { Url = $"{httpReq.Scheme}://staging-api.jgp.com/notemaster" }
                            };
                    });
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "JGP NoteMaster V1");
                    c.RoutePrefix = string.Empty;
                });
            }
        }


        /// <summary>
        ///     Schemas the identifier strategy.
        /// </summary>
        /// <param name="currentClass">The current class.</param>
        /// <returns>System.String.</returns>
        private static string SchemaIdStrategy(Type currentClass)
        {
            var returnedValue = currentClass.Name;

            if (returnedValue.Contains("Model"))
                returnedValue = returnedValue.Replace("Model", string.Empty);
            if (returnedValue.Contains("Dto"))
                returnedValue = returnedValue.Replace("Dto", string.Empty);
            return returnedValue;
        }
    }

    /// <summary>
    ///     Class RemovePagedDataFilter.
    ///     Implements the <see cref="Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter" />
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter" />
    internal class RemovePagedDataFilter : IDocumentFilter
    {
        /// <summary>
        ///     Applies the specified swagger document.
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var keys = swaggerDoc.Components.Schemas
                .Where(x => x.Key.Contains("PagedData`1") || x.Key.Contains("Remove")).ToList();
            keys.ForEach(x => { swaggerDoc.Components.Schemas.Remove(x.Key); });
        }
    }


    /// <summary>
    ///     Class RemoveVersionFromParameter.
    ///     Implements the <see cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter" />
    internal class RemoveVersionFromParameter : IOperationFilter
    {
        /// <summary>
        ///     Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!operation.Parameters.Any())
                return;

            var versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }

    /// <summary>
    ///     Class ReplaceVersionWithExactValueInPath.
    ///     Implements the <see cref="Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter" />
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter" />
    internal class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        /// <summary>
        ///     Applies the specified swagger document.
        /// </summary>
        /// <param name="swaggerDoc">The swagger document.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
                paths.Add(key.Replace("v{version}", swaggerDoc.Info.Version), value);

            swaggerDoc.Paths = paths;
        }
    }
}