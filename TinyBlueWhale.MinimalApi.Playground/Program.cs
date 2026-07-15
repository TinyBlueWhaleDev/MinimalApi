using TinyBlueWhale.MinimalApi.Extensions;
using TinyBlueWhale.MinimalApi.Playground.Configuration;
using TinyBlueWhale.MinimalApi.Versioning.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTinyBlueWhaleMinimalApi<ApplicationApiVersionRegistry>(typeof(Program).Assembly);

builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    var registry = app.Services
        .GetRequiredService<ApiVersionRegistry>();

    foreach (var version in registry.Versions)
    {
        var groupName =
            ApiVersionGroupNameFormatter.Format(version);

        options.SwaggerEndpoint(
            $"/swagger/{groupName}/swagger.json",
            $"TinyBlueWhale.MinimalApi.Playground {groupName}");
    }

    options.RoutePrefix = string.Empty;
});

app.MapGet("/status", () => Results.Ok(new
{
    Application = "TinyBlueWhale.MinimalApi.Playground",
    Status = "Running"
}))
.WithName("Status")
.WithTags("Root")
.WithSummary("Gets playground status")
.Produces(StatusCodes.Status200OK);

app.MapTinyBlueWhaleMinimalApi();

app.Run();
