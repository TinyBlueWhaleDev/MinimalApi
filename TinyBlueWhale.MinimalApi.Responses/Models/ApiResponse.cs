using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyBlueWhale.MinimalApi.Responses.Models
{
    /// <summary>
    /// Represents a standardized API response without data.
    /// </summary>
    public sealed record ApiResponse(string? Message = null);

    /// <summary>
    /// Represents a standardized API response with data.
    /// </summary>
    /// <typeparam name="T">
    /// Response data type.
    /// </typeparam>
    public sealed record ApiResponse<T>(T? Data, string? Message = null);

    /// <summary>
    /// Represents a standardized paged API response.
    /// </summary>
    /// <typeparam name="T">
    /// Collection item type.
    /// </typeparam>
    public sealed record ApiPagedResponse<T>(IReadOnlyCollection<T> Data, PaginationMetadata Pagination, string? Message = null);
}
