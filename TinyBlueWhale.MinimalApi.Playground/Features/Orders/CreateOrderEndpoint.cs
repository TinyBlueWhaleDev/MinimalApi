using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;
using TinyBlueWhale.MinimalApi.Playground.Configuration;

namespace TinyBlueWhale.MinimalApi.Playground.Features.Orders
{
    [Endpoint("Orders")]
    public sealed class CreateOrderEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", (CreateOrderRequest request) =>
            {
                return Microsoft.AspNetCore.Http.Results.Created($"/orders/1001", new
                {
                    Id = 1001,
                    request.Total
                });
            })
            .MapToApiVersion(ApplicationApiVersionRegistry.V1)
            .WithName("CreateOrder")
            .WithTags("Orders")
            .WithSummary("Creates a new order")
            .Produces(StatusCodes.Status201Created);
        }

        public sealed record CreateOrderRequest(decimal Total);
    }
}
