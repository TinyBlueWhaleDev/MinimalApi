using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyBlueWhale.MinimalApi.Versioning.Extensions
{
    /// <summary>
    /// Provides route handler API versioning extensions.
    /// </summary>
    public static class RouteHandlerBuilderExtensions
    {
        /// <summary>
        /// Applies an API version to the route handler.
        /// </summary>
        /// <param name="builder">
        /// Route handler builder.
        /// </param>
        /// <param name="majorVersion">
        /// Major API version number.
        /// </param>
        /// <param name="minorVersion">
        /// Minor API version number.
        /// </param>
        /// <returns>
        /// Configured route handler builder.
        /// </returns>
        public static RouteHandlerBuilder HasApiVersion(this RouteHandlerBuilder builder, int majorVersion, int minorVersion = 0)
        {
            ArgumentNullException.ThrowIfNull(builder);

            return builder.HasApiVersion(new ApiVersion(majorVersion, minorVersion));
        }
    }
}
