using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Configuration;

namespace TinyBlueWhale.MinimalApi.Endpoints.Extensions
{
    /// <summary>
    /// Provides dependency injection extensions for endpoint modules.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers endpoint modules from the specified assemblies.
        /// </summary>
        /// <param name="services">
        /// Service collection where endpoint modules will be registered.
        /// </param>
        /// <param name="assemblies">
        /// Assemblies used to discover endpoint modules.
        /// </param>
        /// <returns>
        /// The configured service collection.
        /// </returns>
        public static IServiceCollection AddEndpoints(this IServiceCollection services, params Assembly[] assemblies)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(assemblies);

            var serviceDescriptors = assemblies
                .Where(assembly => assembly is not null)
                .SelectMany(assembly => assembly.DefinedTypes)
                .Where(type =>
                    type is { IsAbstract: false, IsInterface: false } &&
                    type.IsAssignableTo(typeof(IEndpoint)))
                    .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))

                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            services.AddOptions<EndpointOptions>().BindConfiguration("TinyBlueWhale:MinimalApi:Endpoints");

            return services;
        }
    }
}
