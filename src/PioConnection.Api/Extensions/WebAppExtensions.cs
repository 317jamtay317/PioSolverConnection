using PioConnection.Api.EndPoints;

namespace PioConnection.Api.Extensions;

public static class WebAppExtensions
{
    public static void RegisterEndpoints(this WebApplication app, IEnumerable<IEndpoint> endpoints)
    {
        foreach (var endpoint in endpoints)
        {
            endpoint.RegisterEndpoints(app);
        }
    }

    public static IEnumerable<IEndpoint> GetEndpoints(this WebApplicationBuilder builder)
    {
        var endpoints = typeof(Program)
            .Assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(IEndpoint)))
            .Where(x => x.IsClass)
            .Where(x => !x.IsAbstract)
            .Select(x=> (IEndpoint)Activator.CreateInstance(x))
            .ToArray();
        return endpoints;
    }

    public static void RegisterEndpointServices(this IServiceCollection services, IEnumerable<IEndpoint> endpoints)
    {
        foreach (var endpoint in endpoints)
        {
            endpoint.RegisterServices(services);
        }
    }
}