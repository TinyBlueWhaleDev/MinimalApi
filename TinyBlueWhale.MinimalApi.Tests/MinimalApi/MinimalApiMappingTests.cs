using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Extensions;
using TinyBlueWhale.MinimalApi.Tests.Endpoints;

namespace TinyBlueWhale.MinimalApi.Tests.MinimalApi
{
    public sealed class MinimalApiMappingTests
    {
        [Test]
        public async Task MapTinyBlueWhaleMinimalApi_ShouldMapDefaultV1Endpoint()
        {
            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();

            builder.Services.AddTinyBlueWhaleMinimalApi(
                typeof(TestEndpoint).Assembly);

            var app = builder.Build();

            app.MapTinyBlueWhaleMinimalApi();

            await app.StartAsync();

            try
            {
                var client = app.GetTestClient();

                var response = await client.GetAsync(
                    "/api/v1/test");

                Assert.That(
                    response.IsSuccessStatusCode,
                    Is.True);
            }
            finally
            {
                await app.StopAsync();
                await app.DisposeAsync();
            }
        }

    }
}
