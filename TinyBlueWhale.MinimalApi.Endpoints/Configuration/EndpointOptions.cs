using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyBlueWhale.MinimalApi.Endpoints.Configuration
{
    /// <summary>
    /// Represents Minimal API endpoint module options.
    /// </summary>
    public sealed class EndpointOptions
    {
        /// <summary>
        /// Gets the endpoint module names excluded from mapping.
        /// </summary>
        public List<string> Excluded { get; init; } = [];
    }
}
