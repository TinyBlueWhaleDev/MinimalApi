using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TinyBlueWhale.MinimalApi.Endpoints.Extensions;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;
using TinyBlueWhale.MinimalApi.Versioning.Extensions;

namespace TinyBlueWhale.MinimalApi.Extensions
{
    /// <summary>
    /// Provides dependency injection extensions for the
    /// TinyBlueWhale Minimal API framework.
    /// </summary>
    public static class MinimalApiServiceCollectionExtensions
    {
        /// <summary>
        /// Registers modular endpoints using the default API version registry.
        /// </summary>
        /// <remarks>
        /// The default registry configures API version 1.0.
        /// </remarks>
        /// <param name="services">
        /// Service collection where framework services are registered.
        /// </param>
        /// <param name="endpointAssemblies">
        /// Assemblies containing endpoint modules.
        /// </param>
        /// <returns>
        /// The configured service collection.
        /// </returns>
        public static IServiceCollection AddTinyBlueWhaleMinimalApi(
            this IServiceCollection services,
            params Assembly[] endpointAssemblies)
        {
            return AddTinyBlueWhaleMinimalApiCore<DefaultApiVersionRegistry>(
                services,
                endpointAssemblies);
        }

        /// <summary>
        /// Registers modular endpoints using a custom API version registry.
        /// </summary>
        /// <typeparam name="TRegistry">
        /// API version registry used as the source of truth.
        /// </typeparam>
        /// <param name="services">
        /// Service collection where framework services are registered.
        /// </param>
        /// <param name="endpointAssemblies">
        /// Assemblies containing endpoint modules.
        /// </param>
        /// <returns>
        /// The configured service collection.
        /// </returns>
        public static IServiceCollection AddTinyBlueWhaleMinimalApi<TRegistry>(
            this IServiceCollection services,
            params Assembly[] endpointAssemblies)
            where TRegistry : ApiVersionRegistry, new()
        {
            return AddTinyBlueWhaleMinimalApiCore<TRegistry>(
                services,
                endpointAssemblies);
        }

        private static IServiceCollection AddTinyBlueWhaleMinimalApiCore<TRegistry>(
            IServiceCollection services,
            Assembly[] endpointAssemblies)
            where TRegistry : ApiVersionRegistry, new()
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(endpointAssemblies);

            var assemblies = endpointAssemblies
                .Where(static assembly => assembly is not null)
                .Distinct()
                .ToArray();

            if (assemblies.Length == 0)
            {
                throw new ArgumentException(
                    "At least one endpoint assembly must be provided.",
                    nameof(endpointAssemblies));
            }

            services.AddEndpointsApiExplorer();
            services.AddEndpoints(assemblies);

            services
                .AddApiVersioningFromRegistry<TRegistry>()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }

        private sealed class DefaultApiVersionRegistry : ApiVersionRegistry
        {
            public DefaultApiVersionRegistry()
            {
            }
        }
    }
}
