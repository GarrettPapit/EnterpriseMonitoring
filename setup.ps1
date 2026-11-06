# 1. Create root and project directories
New-Item -ItemType Directory -Force -Path "EnterpriseMonitoring/src/Backend" | Out-Null
Set-Location "EnterpriseMonitoring/src/Backend"

# 2. Create the solution
dotnet new sln -n EnterpriseMonitor

# 3. Create the projects
dotnet new webapi -n EnterpriseMonitor.Api
dotnet new classlib -n EnterpriseMonitor.Core
dotnet new classlib -n EnterpriseMonitor.Infrastructure

# 4. Add projects to the solution file
dotnet sln EnterpriseMonitor.sln add EnterpriseMonitor.Api/EnterpriseMonitor.Api.csproj
dotnet sln EnterpriseMonitor.sln add EnterpriseMonitor.Core/EnterpriseMonitor.Core.csproj
dotnet sln EnterpriseMonitor.sln add EnterpriseMonitor.Infrastructure/EnterpriseMonitor.Infrastructure.csproj

# 5. Set up project dependency references
dotnet add EnterpriseMonitor.Api/EnterpriseMonitor.Api.csproj reference EnterpriseMonitor.Core/EnterpriseMonitor.Core.csproj
dotnet add EnterpriseMonitor.Api/EnterpriseMonitor.Api.csproj reference EnterpriseMonitor.Infrastructure/EnterpriseMonitor.Infrastructure.csproj
dotnet add EnterpriseMonitor.Infrastructure/EnterpriseMonitor.Infrastructure.csproj reference EnterpriseMonitor.Core/EnterpriseMonitor.Core.csproj

# 6. Install Core packages (MediatR & Validation)
Set-Location "EnterpriseMonitor.Core"
dotnet add package MediatR
dotnet add package FluentValidation.DependencyInjectionExtensions

# 7. Install Infrastructure packages (Dapper & SQL Client)
Set-Location "../EnterpriseMonitor.Infrastructure"
dotnet add package Dapper
dotnet add package Microsoft.Data.SqlClient

# 8. Install API packages (SignalR)
Set-Location "../EnterpriseMonitor.Api"
dotnet add package Microsoft.AspNetCore.SignalR

# Return to root Backend folder
Set-Location ".."
Write-Host "[SUCCESS] .NET 8 Enterprise Activity Monitor solution structure and packages initialized!" -ForegroundColor Green