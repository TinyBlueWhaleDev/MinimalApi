using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyBlueWhale.MinimalApi.Responses.Models;
using TinyBlueWhale.MinimalApi.Responses.Results;

namespace TinyBlueWhale.MinimalApi.Tests.Responses
{
    public sealed class ApiResultsTests
    {
        [Test]
        public async Task Ok_ShouldReturnApiResponseWithData()
        {
            var result = ApiResults.Ok(
                new TestUser(
                    1,
                    "Marco"));

            var response = await ExecuteAsync(result);

            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(response.Body, Does.Contain("\"data\""));
            Assert.That(response.Body, Does.Contain("\"Marco\""));
        }

        [Test]
        public async Task Message_ShouldReturnApiResponseWithoutData()
        {
            var result = ApiResults.Message("Operation completed successfully.");

            var response = await ExecuteAsync(result);

            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(response.Body, Does.Contain("\"message\""));
            Assert.That(response.Body, Does.Contain("Operation completed successfully."));
        }

        [Test]
        public async Task Created_ShouldReturnCreatedResponse()
        {
            var result = ApiResults.Created(
                "/users/1",
                new TestUser(
                    1,
                    "Marco"),
                "User created successfully.");

            var response = await ExecuteAsync(result);

            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
            Assert.That(response.Headers.Location, Is.EqualTo("/users/1"));
            Assert.That(response.Body, Does.Contain("\"data\""));
            Assert.That(response.Body, Does.Contain("User created successfully."));
        }

        [Test]
        public async Task Accepted_ShouldReturnAcceptedResponse()
        {
            var result = ApiResults.Accepted(
                new
                {
                    TrackingId = "track-001"
                },
                "Request accepted.");

            var response = await ExecuteAsync(result);

            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status202Accepted));
            Assert.That(response.Body, Does.Contain("track-001"));
            Assert.That(response.Body, Does.Contain("Request accepted."));
        }

        [Test]
        public async Task NoContent_ShouldReturnNoContent()
        {
            var result = ApiResults.NoContent();

            var response = await ExecuteAsync(result);

            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status204NoContent));
            Assert.That(response.Body, Is.Empty);
        }

        [Test]
        public async Task Paged_ShouldReturnPaginationMetadata()
        {
            var users = new[]
            {
            new TestUser(1, "Marco"),
            new TestUser(2, "TinyBlueWhale")
        };

            var pagination = new PaginationMetadata(
                Page: 1,
                PageSize: 2,
                TotalRecords: 10);

            var result = ApiResults.Paged(
                users,
                pagination,
                "Users retrieved successfully.");

            var response = await ExecuteAsync(result);

            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
            Assert.That(response.Body, Does.Contain("\"data\""));
            Assert.That(response.Body, Does.Contain("\"pagination\""));
            Assert.That(response.Body, Does.Contain("\"totalPages\""));
            Assert.That(response.Body, Does.Contain("Users retrieved successfully."));
        }

        [Test]
        public async Task Message_ShouldAllowNullMessage()
        {
            var result = ApiResults.Message();

            var response = await ExecuteAsync(result);

            Assert.That(response.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public void Created_ShouldThrowWhenUriIsEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                ApiResults.Created(
                    string.Empty,
                    new TestUser(
                        1,
                        "Marco")));
        }

        [Test]
        public void PaginationMetadata_ShouldCalculateTotalPages()
        {
            var pagination = new PaginationMetadata(
                Page: 1,
                PageSize: 20,
                TotalRecords: 95);

            Assert.That(pagination.TotalPages, Is.EqualTo(5));
        }

        [Test]
        public void PaginationMetadata_ShouldReturnZeroPagesWhenPageSizeIsInvalid()
        {
            var pagination = new PaginationMetadata(
                Page: 1,
                PageSize: 0,
                TotalRecords: 95);

            Assert.That(pagination.TotalPages, Is.EqualTo(0));
        }

        private static async Task<TestHttpResponse> ExecuteAsync(IResult result)
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddOptions()
                .BuildServiceProvider();

            var context = new DefaultHttpContext
            {
                RequestServices = services
            };

            context.Response.Body = new MemoryStream();

            await result.ExecuteAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(context.Response.Body);

            var body = await reader.ReadToEndAsync();

            return new TestHttpResponse(
                context.Response.StatusCode,
                context.Response.Headers,
                body);
        }
        private sealed record TestUser(
            int Id,
            string Name);

        private sealed record TestHttpResponse(
            int StatusCode,
            IHeaderDictionary Headers,
            string Body);
    }
}
