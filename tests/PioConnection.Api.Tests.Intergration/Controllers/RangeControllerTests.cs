using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using PioConnection.Api.Dtos;
using PioConnection.Api.Requests;
using PioConnection.Dtos;
using Swagger.Internal;

namespace PioConnection.Api.Tests.Intergration.Controllers;

public class RangeControllerTests(CustomWebApplicationFactory<Program> factory) 
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    [Fact]
    public async Task GetFlopRange_ShouldReturnCorrectRange_WhenAsked()
    {
        //arrange
        factory.SolverConnection.GetResponseFromSolver("").Returns(
            ["JJ+,AQs+,AKo"]
        );
        var request = new FlopRangeRequest()
        {
            Position = PlayPosition.OOP,
            FilePath = "C://",
            FlopActions = null
        };
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(request),
            Encoding.UTF8,
            MediaTypes.ApplicationJson);
        //act
        var response = await _client.PostAsync("/range/get-flop", stringContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(responseContent);
        
        //assert
        response.IsSuccessStatusCode.Should().BeTrue();
        apiResponse.IsSuccess.Should().BeTrue();
        apiResponse.Data.Should().Be("JJ+,AQs+,AKo");
        apiResponse.Errors.Should().BeEmpty();
    }
    
    private readonly HttpClient _client = factory.CreateClient();
}