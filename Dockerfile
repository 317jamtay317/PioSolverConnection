# Use the official .NET SDK image for Windows to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0-windowsservercore-ltsc2022 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files for caching and restoring dependencies
COPY ["src/PioConnection.Api/PioConnection.Api.csproj", "src/PioConnection.Api/"]
COPY ["src/C_sharp_2_0/PioConnection.ConnectionDetails.csproj", "src/C_sharp_2_0/"]
COPY ["src/PioConnection.Commands/PioConnection.Commands.csproj", "src/PioConnection.Commands/"]
COPY ["src/PioConnection.Dtos/PioConnection.Dtos.csproj", "src/PioConnection.Dtos/"]

# Restore dependencies
RUN dotnet restore "src/PioConnection.Api/PioConnection.Api.csproj"

# Copy the remaining source code after the restore
COPY . .

# Build the application
WORKDIR "/src/src/PioConnection.Api"
RUN dotnet build "PioConnection.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
RUN dotnet publish "PioConnection.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the official .NET runtime image for Windows to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0-windowsservercore-ltsc2022 AS runtime
WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Expose ports if necessary
EXPOSE 80
EXPOSE 443

# Set the entry point to run the Web API
ENTRYPOINT ["dotnet", "PioConnection.Api.dll"]
