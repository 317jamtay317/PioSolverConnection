namespace PioConnection.Api.EndPoints;

public interface IEndpoint
{
    /// <summary>
    /// Register the endpoints
    /// </summary>
    public Task RegisterEndpoints(WebApplication host);

    /// <summary>
    /// Register the services needed for this endpoint 
    /// </summary>
    public void RegisterServices(IServiceCollection services);
}