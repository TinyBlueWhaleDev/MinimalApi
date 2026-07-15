using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;
using TinyBlueWhale.MinimalApi.Endpoints.Configuration;

namespace TinyBlueWhale.MinimalApi.Endpoints.Extensions
{
    /// <summary>
    /// Provides route builder extensions for endpoint modules.
    /// </summary>
    public static class EndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Maps all registered endpoint modules.
        /// </summary>
        /// <param name="app">
        /// Endpoint route builder where endpoint modules will be mapped.
        /// </param>
        /// <returns>
        /// The configured endpoint route builder.
        /// </returns>
        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app);

            using var scope = app.ServiceProvider.CreateScope();

            var options = scope.ServiceProvider.GetRequiredService<IOptions<EndpointOptions>>().Value;

            var endpoints = scope.ServiceProvider.GetRequiredService<IEnumerable<IEndpoint>>();

            foreach (var endpoint in endpoints)
            {
                if (ShouldSkip(endpoint, options))
                    continue;

                endpoint.MapEndpoint(app);
            }

            return app;
        }

        private static bool ShouldSkip(IEndpoint endpoint,EndpointOptions options)
        {
            var endpointType = endpoint.GetType();

            var attribute = Attribute.GetCustomAttribute(endpointType, typeof(EndpointAttribute)) as EndpointAttribute;

            return attribute is not null && options.Excluded.Contains(attribute.Name, StringComparer.OrdinalIgnoreCase);
        }
    }
}
