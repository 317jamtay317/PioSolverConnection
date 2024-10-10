# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the entire source code
COPY . ./

# Restore dependencies using the Docker-specific solution file
RUN dotnet restore PioConnection.Docker.sln

# Build the application
RUN dotnet publish src/PioConnection.Api/PioConnection.Api.csproj -c Release -o /app/publish

# Build runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80

# Run the Web API
ENTRYPOINT ["dotnet", "PioConnection.Api.dll"]
