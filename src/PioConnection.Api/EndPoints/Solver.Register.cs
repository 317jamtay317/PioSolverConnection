using PioConnection.Api.Dtos;

namespace PioConnection.Api.EndPoints;

public partial class Solver : IEndpoint
{
    private const string Route = "solver";
    private const string Tag = "solver";
    public async Task RegisterEndpoints(WebApplication app)
    {
        app.MapPost($"{Route}/get-flop-range", GetRange)
            .Produces<ApiResult<string>>()
            .WithName(nameof(GetRange))
            .WithTags(Tag);
    }

    public void RegisterServices(IServiceCollection services)
    {
        
    }
}