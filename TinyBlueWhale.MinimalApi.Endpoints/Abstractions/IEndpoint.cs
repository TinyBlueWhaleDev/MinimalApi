using Microsoft.AspNetCore.Routing;

namespace TinyBlueWhale.MinimalApi.Endpoints.Abstractions
{
    /// <summary>
    /// Represents a modular ASP.NET Core Minimal API endpoint.
    /// </summary>
    public interface IEndpoint
    {
        /// <summary>
        /// Maps the endpoint routes into the route builder.
        /// </summary>
        /// <param name="app">
        /// Endpoint route builder used to register routes.
        /// </param>
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
