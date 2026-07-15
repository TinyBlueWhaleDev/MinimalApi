using Asp.Versioning;
using Asp.Versioning.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyBlueWhale.MinimalApi.Versioning.Extensions
{
    /// <summary>
    /// Provides API version set builder extensions.
    /// </summary>
    public static class ApiVersionSetBuilderExtensions
    {
        /// <summary>
        /// Adds multiple API versions to the version set builder.
        /// </summary>
        /// <param name="builder">
        /// API version set builder.
        /// </param>
        /// <param name="versions">
        /// API versions to add.
        /// </param>
        /// <returns>
        /// Configured API version set builder.
        /// </returns>
        public static ApiVersionSetBuilder HasApiVersions(this ApiVersionSetBuilder builder, params ApiVersion[] versions)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(versions);

            foreach (var version in versions)
                builder.HasApiVersion(version);

            return builder;
        }

        /// <summary>
        /// Adds multiple major API versions to the version set builder.
        /// </summary>
        /// <param name="builder">
        /// API version set builder.
        /// </param>
        /// <param name="versions">
        /// Major API versions to add.
        /// </param>
        /// <returns>
        /// Configured API version set builder.
        /// </returns>
        public static ApiVersionSetBuilder HasApiVersions(this ApiVersionSetBuilder builder, params int[] versions)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentNullException.ThrowIfNull(versions);

            foreach (var version in versions)
                builder.HasApiVersion(new ApiVersion(version, 0));

            return builder;
        }
    }
}
