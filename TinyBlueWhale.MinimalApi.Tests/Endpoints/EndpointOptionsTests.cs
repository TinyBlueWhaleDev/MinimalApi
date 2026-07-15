using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Endpoints.Extensions;

namespace TinyBlueWhale.MinimalApi.Tests.Endpoints
{
    public sealed class EndpointOptionsTests
    {
        [Test]
        public void AddEndpoints_ShouldBindExcludedEndpointOptions()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["TinyBlueWhale:MinimalApi:Endpoints:Excluded:0"] = "Test"
                    })
                .Build();

            var builder = WebApplication.CreateBuilder();

            builder.Configuration.AddConfiguration(configuration);
            builder.Services.AddEndpoints(typeof(TestEndpoint).Assembly);

            using var app = builder.Build();

            app.MapEndpoints();

            var dataSource = app.Services.GetRequiredService<EndpointDataSource>();

            Assert.That(dataSource.Endpoints, Is.Empty);
        }
    }
}
