# Market Comparison

This repository contains the unified codebase for the **Market Comparison** platform, which provides an Admin portal, a public API, and an authentication/identity service (Auth). Previously these components lived in three separate repositories; they have now been consolidated into a single solution orchestrated by [.NET Aspire](https://learn.microsoft.com/dotnet/aspire/).

## Projects

The solution is located under `src/` and contains the following projects:

| Project | Description |
| ------- | ----------- |
| `Market.Comparison.AppHost` | .NET Aspire orchestration project. Starts the required services and resolves connection strings and service discovery without Docker. |
| `Market.Comparison.ServiceDefaults` | Shared Aspire service defaults (OpenTelemetry, health checks, service discovery). |
| `Market.Comparison.Admin` | Razor Pages ASP.NET Core web application for administration. |
| `Market.Comparison.Api` | ASP.NET Core Minimal APIs project exposing HTTP endpoints. |
| `Market.Comparison.Auth` | Duende IdentityServer identity provider using ASP.NET Core Razor Pages. |
| `Market.Comparison.Auth.Data` | EF Core data access and migrations for the Auth project. |

## Requirements

To build and run this solution you need:

1. Visual Studio 2026 or Visual Studio Code
2. .NET 10.0 SDK
3. Git
4. SQL Server LocalDB or a local SQL Server instance
5. Azure Application Insights (optional)

## Build

The solution uses the new `.slnx` format and Central Package Management. Restore and build the entire solution with:

```bash
dotnet restore src/Market.Comparison.slnx
dotnet build src/Market.Comparison.slnx --configuration Release --no-restore
```

### Run with .NET Aspire

Set `Market.Comparison.AppHost` as the startup project in Visual Studio and press **F5**, or run from the command line:

```bash
dotnet run --project src/Market.Comparison.AppHost
```

The AppHost reads the connection strings from configuration and passes it to the services that require database access. No Docker is required.

## Central Package Management

NuGet package versions are centralized in the following files under `src/`:

- `Directory.Build.props` — shared MSBuild properties such as `TargetFramework`, `Nullable`, `ImplicitUsings`, and analyzer settings.
- `Directory.Packages.props` — single source of truth for all NuGet package versions. `ManagePackageVersionsCentrally` is enabled and version overrides are disabled.
- `nuget.config` — NuGet source mapping configuration.

Individual `.csproj` files no longer specify package versions or common MSBuild properties; they only declare project references, content items, and project-specific settings.

## Test

Unit tests are found in projects with the suffix "Tests". By running these unit tests, any possible errors due to any code modification can be identified.

These unit tests use XUnit v3 as a testing framework. They are compatible with Visual Studio Test Explorer and are run by GitHub Actions.

## Contribute

To contribute to this project, you only need to have the requirements mentioned above, and upload the changes in a branch and pull request using Git :)
