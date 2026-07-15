using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Attributes;

namespace TinyBlueWhale.MinimalApi.Tests.Endpoints
{
    [Endpoint("Test")]
    public sealed class TestEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/test", () => Microsoft.AspNetCore.Http.Results.Ok("ok"));
        }
    }

    [Endpoint("Second")]
    public sealed class SecondTestEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/second", () => Microsoft.AspNetCore.Http.Results.Ok("second"));
        }
    }

    [Endpoint("CaseInsensitive")]
    public sealed class ExcludedCaseInsensitiveEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("/case-insensitive", () => Microsoft.AspNetCore.Http.Results.Ok("ok"));
        }
    }
  
}

