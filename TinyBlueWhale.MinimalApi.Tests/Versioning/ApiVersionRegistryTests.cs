using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

namespace TinyBlueWhale.MinimalApi.Tests.Versioning
{
    public sealed class ApiVersionRegistryTests
    {
        [Test]
        public void Constructor_ShouldRegisterDefaultV1()
        {
            var registry = new TestApiVersionRegistry();

            Assert.That(registry.DefaultVersion, Is.EqualTo(ApiVersionRegistry.V1));
            Assert.That(registry.Versions, Does.Contain(ApiVersionRegistry.V1));
        }

        [Test]
        public void Constructor_ShouldRegisterCustomVersions()
        {
            var registry = new TestApiVersionRegistry();

            Assert.That(registry.Versions, Does.Contain(TestApiVersionRegistry.V2));
            Assert.That(registry.Versions, Does.Contain(TestApiVersionRegistry.V4_8));
        }

        [Test]
        public void Constructor_ShouldAvoidDuplicateVersions()
        {
            var registry = new DuplicateApiVersionRegistry();

            Assert.That(
                registry.Versions.Count(version =>
                    version.MajorVersion == 2 &&
                    version.MinorVersion == 0),
                Is.EqualTo(1));
        }

        private sealed class DuplicateApiVersionRegistry : ApiVersionRegistry
        {
            public DuplicateApiVersionRegistry()
            {
                Add(2);
                Add(2);
            }
        }
    }
}
