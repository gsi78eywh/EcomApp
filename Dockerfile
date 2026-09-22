# ==============================================================================
# Multi-Stage Production Dockerfile for JMT CAFE (LusiTrack) - .NET 8 LTS
# Compatible with Render, Railway, Fly.io, Azure App Service, AWS, Cloud Run
# ==============================================================================

# Stage 1: Build & Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["LusiTrack/LusiTrack.csproj", "LusiTrack/"]
RUN dotnet restore "LusiTrack/LusiTrack.csproj"

# Copy the remaining source files and compile
COPY LusiTrack/ LusiTrack/
WORKDIR "/src/LusiTrack"
RUN dotnet publish "LusiTrack.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime Container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Security: Run as non-root built-in app user
USER $APP_UID

# Expose standard ASP.NET Core container port
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LusiTrack.dll"]
