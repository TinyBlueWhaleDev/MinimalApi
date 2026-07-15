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
    public sealed class ServiceCollectionExtensionsTests
    {
        [Test]
        public void AddApiVersioningFromRegistry_ShouldRegisterBaseRegistry()
        {
            var services = new ServiceCollection();

            services.AddApiVersioningFromRegistry<TestApiVersionRegistry>();

            using var provider = services.BuildServiceProvider();

            var registry = provider.GetRequiredService<ApiVersionRegistry>();

            Assert.That(registry, Is.TypeOf<TestApiVersionRegistry>());
        }

        [Test]
        public void AddApiVersioningFromRegistry_ShouldRegisterConcreteRegistry()
        {
            var services = new ServiceCollection();

            services.AddApiVersioningFromRegistry<TestApiVersionRegistry>();

            using var provider = services.BuildServiceProvider();

            var registry = provider.GetRequiredService<TestApiVersionRegistry>();

            Assert.That(registry.Versions, Does.Contain(TestApiVersionRegistry.V2));
        }
    }
}
