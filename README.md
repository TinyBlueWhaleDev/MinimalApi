# TinyBlueWhale.MinimalApi

> Opinionated framework for building modular and versioned ASP.NET Core Minimal APIs with less infrastructure and more focus on business features.

---

# Contents

- The Problem
- Why TinyBlueWhale.MinimalApi
- Philosophy
- Design Principles
- Features
- Architecture
- Packages
- Installation
- Quick Start
- Playground
- Roadmap
- FAQ
- Project Status
- License

---

# The Problem

ASP.NET Core Minimal APIs dramatically reduce boilerplate, but as applications grow, teams often recreate the same infrastructure over and over again.

Typical projects end up repeating:

- Endpoint registration
- Assembly scanning
- Route grouping
- API version configuration
- ApiExplorer configuration
- Program.cs boilerplate

Every project solves the same problems differently.

The result is inconsistent architecture, duplicated infrastructure and additional maintenance cost.

TinyBlueWhale.MinimalApi standardizes that infrastructure while preserving the explicit programming model of ASP.NET Core.

---

# Why TinyBlueWhale.MinimalApi

TinyBlueWhale.MinimalApi is **not** another web framework.

It is a lightweight opinionated layer built on top of ASP.NET Core Minimal APIs.

Its goal is simple:

- remove repetitive infrastructure
- encourage modular endpoints
- centralize API versioning
- provide consistent conventions
- remain fully compatible with ASP.NET Core

The framework never hides ASP.NET Core.

Instead, it embraces it.

---

# Philosophy

TinyBlueWhale.MinimalApi follows a very simple philosophy.

**Keep ASP.NET Core explicit.**

Infrastructure should disappear.

Business logic should remain obvious.

The framework favors conventions over repetitive code without introducing unnecessary abstractions.

---

# Design Principles

- Explicit over magic
- Convention over repetition
- Modular endpoints
- Vertical Slice friendly
- Versioning by default
- Independent packages
- ASP.NET Core first

---

# Features

## Endpoint Discovery

- Automatic endpoint registration
- Assembly scanning
- Automatic endpoint mapping
- Endpoint exclusions

## Versioning

- Default API Version (v1)
- Custom version registries
- API Explorer integration

## Responses

- Standardized API responses
- Pagination support
- Helper result methods

---

# Architecture

```text
Application
        │
        ▼
AddTinyBlueWhaleMinimalApi()
        │
        ▼
Endpoint Discovery
        │
        ▼
API Version Registry
        │
        ▼
Route Groups
        │
        ▼
Endpoint Mapping
        │
        ▼
ASP.NET Core
```

---

# Packages

| Package | Description |
|----------|-------------|
| TinyBlueWhale.MinimalApi | Framework facade |
| TinyBlueWhale.MinimalApi.Endpoints | Endpoint discovery and mapping |
| TinyBlueWhale.MinimalApi.Versioning | API Versioning |
| TinyBlueWhale.MinimalApi.Responses | Standardized API responses |

---

# Installation

```bash
dotnet add package TinyBlueWhale.MinimalApi --prerelease
```

Compatible with:

- .NET 8
- .NET 9

---

# Quick Start

Register the framework.

```csharp
builder.Services.AddTinyBlueWhaleMinimalApi(
    typeof(Program).Assembly);
```

Map endpoints.

```csharp
app.MapTinyBlueWhaleMinimalApi();
```

Create an endpoint.

```csharp
[Endpoint("Orders")]
public sealed class CreateOrderEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", () =>
        {
            return Results.Ok();
        })
        .MapToApiVersion(ApiVersionRegistry.V1);
    }
}
```

That's it.

The endpoint becomes available at:

```
POST /api/v1/orders
```

---

# Playground

The repository includes a complete Playground project demonstrating:

- endpoint discovery
- API versioning
- Swagger integration
- standardized responses
- package usage

The Playground serves as the reference implementation for every released package.

---

# Roadmap

Current Preview

- Endpoint Discovery
- Endpoint Mapping
- API Version Registry
- Default Versioning
- Responses
- Pagination

Future

- Validation
- Authentication
- Authorization
- Endpoint Filters
- Exception Handling
- OpenTelemetry
- Source Generators

---

# FAQ

### Does TinyBlueWhale.MinimalApi replace ASP.NET Core?

No.

It extends ASP.NET Core.

---

### Does it replace Controllers?

No.

It focuses exclusively on Minimal APIs.

---

### Is API Versioning required?

No.

Version 1 is configured automatically.

---

### Can I create custom API versions?

Yes.

Create your own ApiVersionRegistry.

---

### Does it configure Swagger?

No.

Swagger remains explicit.

---

### Can I use the packages independently?

Yes.

Every package is independently consumable.

---

# Project Status

| Feature | Status |
|----------|--------|
| Endpoint Discovery | ✅ |
| Endpoint Mapping | ✅ |
| Versioning | ✅ |
| Responses | ✅ |
| .NET 8 | ✅ |
| .NET 9 | ✅ |

Current Version

```
1.0.0-preview
```

---

# License

Licensed under the MIT License.

Developed by TinyBlueWhale.