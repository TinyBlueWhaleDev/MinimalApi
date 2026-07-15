using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

namespace TinyBlueWhale.MinimalApi.Versioning.Builders
{
    /// <summary>
    /// Creates API version sets from version registries.
    /// </summary>
    public static class ApiVersionSetFactory
    {
        /// <summary>
        /// Creates an API version set from the provided registry.
        /// </summary>
        public static ApiVersionSet Create(WebApplication app, ApiVersionRegistry registry)
        {
            ArgumentNullException.ThrowIfNull(app);
            ArgumentNullException.ThrowIfNull(registry);

            var builder = app.NewApiVersionSet();

            foreach (var version in registry.Versions)
                builder.HasApiVersion(version);

            return builder
                .ReportApiVersions()
                .Build();
        }
    }
}
