using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyBlueWhale.MinimalApi.Endpoints.Attributes
{
    /// <summary>
    /// Defines metadata for an endpoint module.
    /// </summary>
    /// <param name="name">
    /// Endpoint module name.
    /// </param>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class EndpointAttribute(string name) : Attribute
    {
        /// <summary>
        /// Gets the endpoint module name.
        /// </summary>
        public string Name { get; } = string.IsNullOrWhiteSpace(name)
            ? throw new ArgumentException("Endpoint name cannot be null or whitespace.", nameof(name))
            : name;
    }
}
