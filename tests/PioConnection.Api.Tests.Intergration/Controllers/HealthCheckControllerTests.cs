using FluentAssertions;

namespace PioConnection.Api.Tests.Intergration.Controllers;

public class HealthCheckControllerTests(CustomWebApplicationFactory<Program> factory) 
    : IClassFixture<CustomWebApplicationFactory<global::Program>>
{
    [Fact]
    public async Task HealthCheck_ShouldReturnOk_WhenWorking()
    {
        //arrange

        //act
        var response = await _client.GetAsync("/health/check");
        
        //assert
        response.IsSuccessStatusCode.Should().BeTrue();
    }
    
    private readonly HttpClient _client = factory.CreateClient();
}