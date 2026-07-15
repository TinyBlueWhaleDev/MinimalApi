using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;
using TinyBlueWhale.MinimalApi.Responses.Models;
using TinyBlueWhale.MinimalApi.Responses.Results;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

namespace TinyBlueWhale.MinimalApi.Playground.Features.Responses
{

    [Endpoint("Responses")]
    public sealed class ResponseShowcaseEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/responses/ok", () =>
            {
                return ApiResults.Ok(new
                {
                    Id = 1,
                    Name = "Marco"
                });
            })
            .MapToApiVersion(ApiVersionRegistry.V1)
            .WithName("ResponseOk")
            .WithTags("Responses")
            .WithSummary("Returns a standardized OK response");

            app.MapGet("/responses/message", () =>
            {
                return ApiResults.Message("Operation completed successfully.");
            })
            .MapToApiVersion(ApiVersionRegistry.V1)
            .WithName("ResponseMessage")
            .WithTags("Responses")
            .WithSummary("Returns a standardized message response");

            app.MapPost("/responses/created", () =>
            {
                return ApiResults.Created(
                    "/api/v1/responses/created/1",
                    new
                    {
                        Id = 1,
                        Status = "Created"
                    },
                    "Resource created successfully.");
            })
            .MapToApiVersion(ApiVersionRegistry.V1)
            .WithName("ResponseCreated")
            .WithTags("Responses")
            .WithSummary("Returns a standardized created response");

            app.MapPost("/responses/accepted", () =>
            {
                return ApiResults.Accepted(
                    new
                    {
                        TrackingId = Guid.NewGuid(),
                        Status = "Accepted"
                    },
                    "Request accepted for processing.");
            })
            .MapToApiVersion(ApiVersionRegistry.V1)
            .WithName("ResponseAccepted")
            .WithTags("Responses")
            .WithSummary("Returns a standardized accepted response");

            app.MapDelete("/responses/no-content", () =>
            {
                return ApiResults.NoContent();
            })
            .MapToApiVersion(ApiVersionRegistry.V1)
            .WithName("ResponseNoContent")
            .WithTags("Responses")
            .WithSummary("Returns a no content response");

            app.MapGet("/responses/paged", () =>
            {
                var items = new[]
                {
                new { Id = 1, Name = "Item 1" },
                new { Id = 2, Name = "Item 2" },
                new { Id = 3, Name = "Item 3" }
            };

                return ApiResults.Paged(
                    items,
                    new PaginationMetadata(
                        Page: 1,
                        PageSize: 3,
                        TotalRecords: 25),
                    "Paged data retrieved successfully.");
            })
            .MapToApiVersion(ApiVersionRegistry.V1)
            .WithName("ResponsePaged")
            .WithTags("Responses")
            .WithSummary("Returns a standardized paged response");
        }
    }
}
