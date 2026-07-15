using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

namespace TinyBlueWhale.MinimalApi.Versioning.Extensions
{
    /// <summary>
    /// Provides dependency injection extensions for API versioning.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds API versioning using the provided API version registry.
        /// </summary>
        /// <typeparam name="TRegistry">
        /// API version registry type.
        /// </typeparam>
        /// <param name="services">
        /// Service collection where API versioning services will be registered.
        /// </param>
        /// <returns>
        /// Configured API versioning builder.
        /// </returns>
        public static IApiVersioningBuilder AddApiVersioningFromRegistry<TRegistry>(this IServiceCollection services)
            where TRegistry : ApiVersionRegistry, new()
        {
            ArgumentNullException.ThrowIfNull(services);

            var registry = new TRegistry();

            services.AddSingleton<ApiVersionRegistry>(registry);
            services.AddSingleton<TRegistry>(registry);

            return services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = registry.DefaultVersion;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }
    }
}
