using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;
using TinyBlueWhale.MinimalApi.Versioning.Extensions;

namespace TinyBlueWhale.MinimalApi.Tests.Versioning
{
    public sealed class VersionedEndpointTests
    {
        [Test]
        public async Task VersionedEndpoint_ShouldResolveV1()
        {
            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();

            builder.Services
                .AddApiVersioningFromRegistry<TestApiVersionRegistry>()
                .AddApiExplorer();

            var app = builder.Build();

            var registry = app.Services.GetRequiredService<ApiVersionRegistry>();
            var versionSet = app.BuildVersionSet(registry);

            var api = app
                .MapGroup("/api/v{version:apiVersion}")
                .WithApiVersionSet(versionSet);

            api.MapGet("/users", () => Microsoft.AspNetCore.Http.Results.Ok("v1"))
                .HasApiVersion(ApiVersionRegistry.V1);

            await app.StartAsync();

            var client = app.GetTestClient();

            var response = await client.GetAsync("/api/v1/users");
            var content = await response.Content.ReadAsStringAsync();

            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(content, Does.Contain("v1"));
        }

        [Test]
        public async Task VersionedEndpoint_ShouldResolveV2()
        {
            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();

            builder.Services
                .AddApiVersioningFromRegistry<TestApiVersionRegistry>()
                .AddApiExplorer();

            var app = builder.Build();

            var registry = app.Services.GetRequiredService<ApiVersionRegistry>();
            var versionSet = app.BuildVersionSet(registry);

            var api = app
                .MapGroup("/api/v{version:apiVersion}")
                .WithApiVersionSet(versionSet);

            api.MapGet("/users", () => Microsoft.AspNetCore.Http.Results.Ok("v2"))
                .HasApiVersion(TestApiVersionRegistry.V2);

            await app.StartAsync();

            var client = app.GetTestClient();

            var response = await client.GetAsync("/api/v2/users");
            var content = await response.Content.ReadAsStringAsync();

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode, Is.True);
                Assert.That(content, Does.Contain("v2"));
            });
        }

        [Test]
        public async Task VersionedEndpoint_ShouldResolveMinorVersion()
        {
            var builder = WebApplication.CreateBuilder();

            builder.WebHost.UseTestServer();

            builder.Services
                .AddApiVersioningFromRegistry<TestApiVersionRegistry>()
                .AddApiExplorer();

            var app = builder.Build();

            var registry = app.Services.GetRequiredService<ApiVersionRegistry>();
            var versionSet = app.BuildVersionSet(registry);

            var api = app
                .MapGroup("/api/v{version:apiVersion}")
                .WithApiVersionSet(versionSet);

            api.MapGet("/users", () => Microsoft.AspNetCore.Http.Results.Ok("v4.8"))
                .HasApiVersion(TestApiVersionRegistry.V4_8);

            await app.StartAsync();

            var client = app.GetTestClient();

            var response = await client.GetAsync("/api/v4.8/users");
            var content = await response.Content.ReadAsStringAsync();

            Assert.Multiple(() =>
            {
                Assert.That(response.IsSuccessStatusCode, Is.True);
                Assert.That(content, Does.Contain("v4.8"));
            });
        }
    }
}
