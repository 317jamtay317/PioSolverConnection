# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the solution file and project files for better caching
COPY PioConnection.Docker.sln ./
COPY src/C_sharp_2_0/PioConnection.ConnectionDetails.csproj src/C_sharp_2_0/
COPY src/PioConnection.Dtos/PioConnection.Dtos.csproj src/PioConnection.Dtos/
COPY src/PioConnection.Commands/PioConnection.Commands.csproj src/PioConnection.Commands/
COPY src/PioConnection.Api/PioConnection.Api.csproj src/PioConnection.Api/

# Restore dependencies only if project files have changed
RUN dotnet restore PioConnection.Docker.sln

# Copy the rest of the source code
COPY . ./

# Build the application
RUN dotnet publish src/PioConnection.Api/PioConnection.Api.csproj -c Release -o /app/publish --no-restore

# Build runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80
EXPOSE 57100
EXPOSE 57300

# Run the Web API
ENTRYPOINT ["dotnet", "PioConnection.Api.dll"]
