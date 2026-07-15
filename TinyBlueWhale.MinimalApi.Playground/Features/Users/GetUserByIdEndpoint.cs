using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;
using TinyBlueWhale.MinimalApi.Playground.Configuration;

namespace TinyBlueWhale.MinimalApi.Playground.Features.Users
{
    [Endpoint("Users")]
    public sealed class GetUserByIdEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/{id:int}", (int id) =>
            {
                return Microsoft.AspNetCore.Http.Results.Ok(new
                {
                    Id = id,
                    Name = $"User {id}"
                });
            })
            .MapToApiVersion(ApplicationApiVersionRegistry.V3)
            .WithName("GetUserById")
            .WithTags("Users")
            .WithSummary("Gets a user by id")
            .Produces(StatusCodes.Status200OK);
        }
    }
}
