using Asp.Versioning;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

namespace TinyBlueWhale.MinimalApi.Playground.Configuration
{
    internal sealed class ApplicationApiVersionRegistry : ApiVersionRegistry
    {
        public static readonly ApiVersion V2 = Create(2);
        public static readonly ApiVersion V3 = Create(3);
        public static readonly ApiVersion V4_8 = Create(4, 8);

        public ApplicationApiVersionRegistry()
        {
            Add(V2);
            Add(V3);
            Add(V4_8);
        }
    }
}
