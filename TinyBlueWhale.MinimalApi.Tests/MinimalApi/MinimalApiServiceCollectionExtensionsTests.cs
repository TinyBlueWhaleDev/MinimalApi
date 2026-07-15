using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Endpoints.Abstractions;
using TinyBlueWhale.MinimalApi.Extensions;
using TinyBlueWhale.MinimalApi.Tests.Endpoints;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

namespace TinyBlueWhale.MinimalApi.Tests.MinimalApi
{
    public sealed class MinimalApiServiceCollectionExtensionsTests
    {
        [Test]
        public void AddTinyBlueWhaleMinimalApi_WithDefaultRegistry_ShouldRegisterV1()
        {
            var services = new ServiceCollection();

            services.AddLogging();

            services.AddTinyBlueWhaleMinimalApi(
                typeof(TestEndpoint).Assembly);

            using var provider = services.BuildServiceProvider();

            var registry = provider
                .GetRequiredService<ApiVersionRegistry>();

            Assert.That(registry.Versions.Count, Is.EqualTo(1));

            Assert.That(
                registry.Versions.Any(version =>
                    version.MajorVersion == 1 &&
                    version.MinorVersion == 0),
                Is.True);
        }

        [Test]
        public void AddTinyBlueWhaleMinimalApi_WithCustomRegistry_ShouldRegisterAllVersions()
        {
            var services = new ServiceCollection();

            services.AddLogging();

            services.AddTinyBlueWhaleMinimalApi<TestApiVersionRegistry>(
                typeof(TestEndpoint).Assembly);

            using var provider = services.BuildServiceProvider();

            var registry = provider
                .GetRequiredService<ApiVersionRegistry>();

            Assert.That(registry.Versions.Count, Is.EqualTo(2));

            Assert.Multiple(() =>
            {
                Assert.That(
                            registry.Versions.Any(version =>
                                version.MajorVersion == 1 &&
                                version.MinorVersion == 0),
                            Is.True);

                Assert.That(
                    registry.Versions.Any(version =>
                        version.MajorVersion == 2 &&
                        version.MinorVersion == 0),
                    Is.True);
            });
        }

        [Test]
        public void AddTinyBlueWhaleMinimalApi_WithoutAssemblies_ShouldThrowArgumentException()
        {
            var services = new ServiceCollection();

            var exception = Assert.Throws<ArgumentException>(() =>
                services.AddTinyBlueWhaleMinimalApi());

            Assert.That(
                exception?.ParamName,
                Is.EqualTo("endpointAssemblies"));
        }

        private sealed class TestApiVersionRegistry : ApiVersionRegistry
        {
            public TestApiVersionRegistry()
            {
                Add(2);
            }
        }
    }
}
