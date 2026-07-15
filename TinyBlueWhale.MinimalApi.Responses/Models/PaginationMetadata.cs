using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyBlueWhale.MinimalApi.Responses.Models
{
    /// <summary>
    /// Represents pagination metadata.
    /// </summary>
    public sealed record PaginationMetadata(int Page, int PageSize, long TotalRecords)
    {
        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public long TotalPages => PageSize <= 0
            ? 0
            : (long)Math.Ceiling((double)TotalRecords / PageSize);
    }
}
