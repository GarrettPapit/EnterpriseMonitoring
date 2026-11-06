# Stage 1: Build / Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files for layer caching
COPY ["EnterpriseMonitor.sln", "./"]
COPY ["EnterpriseMonitor.Api/EnterpriseMonitor.Api.csproj", "EnterpriseMonitor.Api/"]
COPY ["EnterpriseMonitor.Core/EnterpriseMonitor.Core.csproj", "EnterpriseMonitor.Core/"]
COPY ["EnterpriseMonitor.Infrastructure/EnterpriseMonitor.Infrastructure.csproj", "EnterpriseMonitor.Infrastructure/"]

RUN dotnet restore "EnterpriseMonitor.Api/EnterpriseMonitor.Api.csproj"

# Copy source code and build
COPY . .
WORKDIR "/src/EnterpriseMonitor.Api"
RUN dotnet build "EnterpriseMonitor.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EnterpriseMonitor.Api.csproj" -c Release -o /app/publish

# Stage 2: Runtime Environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnterpriseMonitor.Api.dll"]