using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using TinyBlueWhale.MinimalApi.Endpoints.Extensions;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;
using TinyBlueWhale.MinimalApi.Versioning.Extensions;

namespace TinyBlueWhale.MinimalApi.Extensions
{
    /// <summary>
    /// Provides route mapping extensions for the
    /// TinyBlueWhale Minimal API framework.
    /// </summary>
    public static class MinimalApiEndpointRouteBuilderExtensions
    {
        private const string DefaultRoutePattern =
            "/api/v{version:apiVersion}";

        /// <summary>
        /// Maps all registered endpoint modules under a versioned route group.
        /// </summary>
        /// <param name="application">
        /// Web application where endpoints are mapped.
        /// </param>
        /// <param name="routePattern">
        /// Versioned route group pattern.
        /// </param>
        /// <returns>
        /// The configured route group.
        /// </returns>
        public static RouteGroupBuilder MapTinyBlueWhaleMinimalApi(
            this WebApplication application,
            string routePattern = DefaultRoutePattern)
        {
            ArgumentNullException.ThrowIfNull(application);

            if (string.IsNullOrWhiteSpace(routePattern))
            {
                throw new ArgumentException(
                    "Route pattern cannot be null or whitespace.",
                    nameof(routePattern));
            }

            var registry = application.Services
                .GetRequiredService<ApiVersionRegistry>();

            var versionSet = application.BuildVersionSet(registry);

            var group = application
                .MapGroup(routePattern)
                .WithApiVersionSet(versionSet);

            group.MapEndpoints();

            return group;
        }
    }
}
