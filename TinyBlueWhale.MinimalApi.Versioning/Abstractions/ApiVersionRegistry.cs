using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyBlueWhale.MinimalApi.Versioning.Abstractions
{
    /// <summary>
    /// Represents an extensible API version registry used as the source of truth for API versions.
    /// </summary>
    public abstract class ApiVersionRegistry
    {
        private readonly List<ApiVersion> _versions = [];

        /// <summary>
        /// Initializes a new instance of the API version registry with version 1.0 enabled by default.
        /// </summary>
        protected ApiVersionRegistry()
        {
            DefaultVersion = V1;
            Add(V1);
        }

        /// <summary>
        /// Gets API version 1.0.
        /// </summary>
        public static readonly ApiVersion V1 = new(1, 0);

        /// <summary>
        /// Gets the default API version.
        /// </summary>
        public ApiVersion DefaultVersion { get; protected set; }

        /// <summary>
        /// Gets the registered API versions.
        /// </summary>
        public IReadOnlyCollection<ApiVersion> Versions => _versions;

        /// <summary>
        /// Creates an API version.
        /// </summary>
        /// <param name="majorVersion">
        /// Major version number.
        /// </param>
        /// <param name="minorVersion">
        /// Minor version number.
        /// </param>
        /// <returns>
        /// Created API version.
        /// </returns>
        protected static ApiVersion Create(int majorVersion, int minorVersion = 0)
        {
            return new ApiVersion(majorVersion, minorVersion);
        }

        /// <summary>
        /// Adds an API version to the registry.
        /// </summary>
        /// <param name="version">
        /// API version to add.
        /// </param>
        protected void Add(ApiVersion version)
        {
            ArgumentNullException.ThrowIfNull(version);

            if (_versions.Any(current =>current.MajorVersion == version.MajorVersion && current.MinorVersion == version.MinorVersion))
                return;

            _versions.Add(version);
        }

        /// <summary>
        /// Creates and adds an API version to the registry.
        /// </summary>
        /// <param name="majorVersion">
        /// Major version number.
        /// </param>
        /// <param name="minorVersion">
        /// Minor version number.
        /// </param>
        /// <returns>
        /// Created API version.
        /// </returns>
        protected ApiVersion Add(int majorVersion, int minorVersion = 0)
        {
            var version = Create(majorVersion, minorVersion);

            Add(version);

            return version;
        }
    }
}
