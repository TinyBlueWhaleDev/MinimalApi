using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Responses.Models;

namespace TinyBlueWhale.MinimalApi.Responses.Results
{
    /// <summary>
    /// Provides standardized Minimal API response helpers.
    /// </summary>
    public static class ApiResults
    {
        /// <summary>
        /// Creates a 200 OK response with data.
        /// </summary>
        public static IResult Ok<T>(T data, string? message = null)
        {
            return Microsoft.AspNetCore.Http.Results.Ok(new ApiResponse<T>(data, message));
        }

        /// <summary>
        /// Creates a 200 OK response with a message.
        /// </summary>
        public static IResult Message(string? message = null)
        {
            return Microsoft.AspNetCore.Http.Results.Ok(new ApiResponse(message));
        }

        /// <summary>
        /// Creates a 201 Created response with data.
        /// </summary>
        public static IResult Created<T>(string uri, T data, string? message = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(uri);

            return Microsoft.AspNetCore.Http.Results.Created(uri, new ApiResponse<T>(data, message));
        }

        /// <summary>
        /// Creates a 202 Accepted response with data.
        /// </summary>
        public static IResult Accepted<T>(T data, string? message = null)
        {
            return Microsoft.AspNetCore.Http.Results.Accepted(value: new ApiResponse<T>(data, message));
        }

        /// <summary>
        /// Creates a 204 No Content response.
        /// </summary>
        public static IResult NoContent()
        {
            return Microsoft.AspNetCore.Http.Results.NoContent();
        }

        /// <summary>
        /// Creates a 200 OK response with paged data.
        /// </summary>
        public static IResult Paged<T>(IReadOnlyCollection<T> data, PaginationMetadata pagination, string? message = null)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentNullException.ThrowIfNull(pagination);

            return Microsoft.AspNetCore.Http.Results.Ok(new ApiPagedResponse<T>(data, pagination, message));
        }
    }
}
