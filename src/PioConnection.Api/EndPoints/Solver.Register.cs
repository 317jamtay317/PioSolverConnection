using PioConnection.Api.Dtos;

namespace PioConnection.Api.EndPoints;

public partial class Solver : IEndpoint
{
    private const string Route = "solver";
    private const string Tag = "solver";
    public async Task RegisterEndpoints(WebApplication app)
    {
        app.MapPost($"{Route}/get-flop-range", GetFlopRange)
            .Produces<ApiResult<string>>()
            .WithName(nameof(GetFlopRange))
            .WithTags(Tag);
    }

    public void RegisterServices(IServiceCollection services)
    {
        
    }
}