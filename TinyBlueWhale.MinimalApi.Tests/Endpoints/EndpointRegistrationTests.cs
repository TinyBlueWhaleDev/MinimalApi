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
    public sealed class EndpointRegistrationTests
    {
        [Test]
        public void AddEndpoints_ShouldRegisterEndpointTypes()
        {
            var services = new ServiceCollection();

            services.AddEndpoints(typeof(TestEndpoint).Assembly);

            using var provider = services.BuildServiceProvider();

            var endpoints = provider.GetServices<IEndpoint>().ToList();

            Assert.That(endpoints[0], Is.TypeOf<TestEndpoint>());
        }
    }
}
