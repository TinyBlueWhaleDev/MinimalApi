using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;
using TinyBlueWhale.MinimalApi.Playground.Configuration;

namespace TinyBlueWhale.MinimalApi.Playground.Features.Users
{
    [Endpoint("Users")]
    public sealed class ListUsersEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/users", () =>
            {
                var users = new[]
                {
                new { Id = 1, Name = "Marco" },
                new { Id = 2, Name = "HadalisTech" }
            };

                return Microsoft.AspNetCore.Http.Results.Ok(users);
            })
            .MapToApiVersion(ApplicationApiVersionRegistry.V2)
            .WithName("ListUsers")
            .WithTags("Users")
            .WithSummary("Gets all users")
            .WithDescription("Returns the list of playground users.")
            .Produces(StatusCodes.Status200OK);
        }
    }
}
