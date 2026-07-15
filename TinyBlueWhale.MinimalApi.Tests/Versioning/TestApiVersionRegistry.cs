using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

namespace TinyBlueWhale.MinimalApi.Tests.Versioning
{
    public sealed class TestApiVersionRegistry : ApiVersionRegistry
    {
        public static readonly ApiVersion V2 = Create(2);
        public static readonly ApiVersion V4_8 = Create(4, 8);

        public TestApiVersionRegistry()
        {
            Add(V2);
            Add(V4_8);
        }
    }
}
