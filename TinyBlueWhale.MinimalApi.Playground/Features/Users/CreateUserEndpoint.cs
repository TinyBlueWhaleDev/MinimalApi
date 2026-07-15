using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;
using TinyBlueWhale.MinimalApi.Responses.Results;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

namespace TinyBlueWhale.MinimalApi.Playground.Features.Users
{
    [Endpoint("Users")]
    public sealed class CreateUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/users", (CreateUserRequest request) =>
            {                
                return Microsoft.AspNetCore.Http.Results.Created($"/users/1", new
                {
                    Id = 1,
                    request.Name
                });
            })
            .MapToApiVersion(ApiVersionRegistry.V1)
            .WithName("CreateUser")
            .WithTags("Users")
            .WithSummary("Creates a new user")
            .Produces(StatusCodes.Status201Created);
        }

        public sealed record CreateUserRequest(string Name);
    }
}
