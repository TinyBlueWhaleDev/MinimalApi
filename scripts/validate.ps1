$ErrorActionPreference = "Stop"

Write-Host "==> .NET SDK"
dotnet --version

Write-Host "==> Restore"
dotnet restore

Write-Host "==> Build"
dotnet build --configuration Release --no-restore

Write-Host "==> Tests"
dotnet test --configuration Release --no-build

Write-Host "==> Vulnerabilities"
dotnet list package --vulnerable --include-transitive

Write-Host "==> Pack"
dotnet pack ./TinyBlueWhale.MinimalApi/TinyBlueWhale.MinimalApi.csproj --configuration Release --no-build
dotnet pack ./TinyBlueWhale.MinimalApi.Endpoints/TinyBlueWhale.MinimalApi.Endpoints.csproj --configuration Release --no-build
dotnet pack ./TinyBlueWhale.MinimalApi.Responses/TinyBlueWhale.MinimalApi.Responses.csproj --configuration Release --no-build
dotnet pack ./TinyBlueWhale.MinimalApi.Versioning/TinyBlueWhale.MinimalApi.Versioning.csproj --configuration Release --no-build

Write-Host "==> Validation completed successfully"