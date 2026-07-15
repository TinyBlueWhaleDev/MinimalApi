using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Endpoints.Extensions;

namespace TinyBlueWhale.MinimalApi.Tests.Endpoints
{
    public sealed class EndpointMappingTests
    {
        [Test]
        public async Task MapEndpoints_ShouldMapRegisteredEndpoint()
        {
            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();
            builder.Services.AddEndpoints(typeof(TestEndpoint).Assembly);

            var app = builder.Build();

            app.MapEndpoints();

            var endpoints = builder.Services.Where(d => d.ServiceType == typeof(IEndpoint)).ToList();

            await app.StartAsync();

            var client = app.GetTestClient();

            var response = await client.GetAsync("/test");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test]
        public void AddEndpoints_ShouldRegisterMultipleEndpointTypes()
        {
            var services = new ServiceCollection();

            services.AddEndpoints(typeof(TestEndpoint).Assembly);

            using var provider = services.BuildServiceProvider();

            var endpoints = provider
                .GetServices<IEndpoint>()
                .ToList();

            Assert.That(endpoints.Count, Is.GreaterThanOrEqualTo(2));
            Assert.That(endpoints.Any(endpoint => endpoint is TestEndpoint), Is.True);
            Assert.That(endpoints.Any(endpoint => endpoint is SecondTestEndpoint), Is.True);
        }

        [Test]
        public async Task MapEndpoints_ShouldRespectExcludedEndpoint()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["TinyBlueWhale:MinimalApi:Endpoints:Excluded:0"] = "Test"
                    })
                .Build();

            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();
            builder.Configuration.AddConfiguration(configuration);
            builder.Services.AddEndpoints(typeof(TestEndpoint).Assembly);

            var app = builder.Build();

            app.MapEndpoints();

            await app.StartAsync();

            var client = app.GetTestClient();

            var response = await client.GetAsync("/test");

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }

        [Test]
        public async Task MapEndpoints_ShouldRespectExcludedEndpointCaseInsensitive()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["TinyBlueWhale:MinimalApi:Endpoints:Excluded:0"] = "caseinsensitive"
                    })
                .Build();

            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();
            builder.Configuration.AddConfiguration(configuration);
            builder.Services.AddEndpoints(typeof(ExcludedCaseInsensitiveEndpoint).Assembly);

            var app = builder.Build();

            app.MapEndpoints();

            await app.StartAsync();

            var client = app.GetTestClient();

            var response = await client.GetAsync("/case-insensitive");

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }

        [Test]
        public async Task MapEndpoints_ShouldMapEndpointInsideRouteGroup()
        {
            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();
            builder.Services.AddEndpoints(typeof(TestEndpoint).Assembly);

            var app = builder.Build();

            var group = app.MapGroup("/api/v1");

            group.MapEndpoints();

            await app.StartAsync();

            var client = app.GetTestClient();

            var response = await client.GetAsync("/api/v1/test");

            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test]
        public async Task MapEndpoints_ShouldNotFailWithoutRegisteredEndpoints()
        {
            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();

            var app = builder.Build();

            app.MapEndpoints();

            await app.StartAsync();

            var client = app.GetTestClient();

            var response = await client.GetAsync("/missing");

            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}
