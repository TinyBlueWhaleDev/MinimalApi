using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;
using TinyBlueWhale.MinimalApi.Playground.Configuration;

namespace TinyBlueWhale.MinimalApi.Playground.Features.Internal
{

    [Endpoint("Internal")]
    public sealed class InternalHealthEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/internal/health", () =>
            {
                return Microsoft.AspNetCore.Http.Results.Ok(new
                {
                    Status = "Healthy"
                });
            })
            .MapToApiVersion(ApplicationApiVersionRegistry.V3)
            .WithName("InternalHealth")
            .WithTags("Internal")
            .WithSummary("Internal health endpoint")
            .Produces(StatusCodes.Status200OK);
        }
    }
}
