using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;
using TinyBlueWhale.MinimalApi.Versioning.Builders;

namespace TinyBlueWhale.MinimalApi.Versioning.Extensions
{
    /// <summary>
    /// Provides extensions for API version registries.
    /// </summary>
    public static class ApiVersionRegistryExtensions
    {
        /// <summary>
        /// Builds an API version set from the provided registry.
        /// </summary>
        /// <param name="app">
        /// Web application used to create the version set.
        /// </param>
        /// <param name="registry">
        /// API version registry.
        /// </param>
        /// <returns>
        /// Configured API version set.
        /// </returns>
        public static ApiVersionSet BuildVersionSet(this WebApplication app, ApiVersionRegistry registry)
        {
            return ApiVersionSetFactory.Create(app, registry);
        }
    }
}
