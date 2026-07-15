using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;
using TinyBlueWhale.MinimalApi.Playground.Configuration;

namespace TinyBlueWhale.MinimalApi.Playground.Features.Orders
{
    [Endpoint("Orders")]
    public sealed class ListOrdersEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", () =>
            {
                var orders = new[]
                {
                new { Id = 1001, Total = 250.75m },
                new { Id = 1002, Total = 980.50m }
            };

                return Microsoft.AspNetCore.Http.Results.Ok(orders);
            })
            .MapToApiVersion(ApplicationApiVersionRegistry.V4_8)
            .WithName("ListOrders")
            .WithTags("Orders")
            .WithSummary("Gets all orders")
            .Produces(StatusCodes.Status200OK);
        }
    }
}
