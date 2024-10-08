using System.Text;
using Client.Plugins;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NSubstitute;
using PioConnection.Api.Dtos;
using PioConnection.Api.Requests;
using PioConnection.Commands;
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
        string[] returnRange =
        [
            "AsAd 1.000 0.000 0.000 0.000",
            "AcAd 1.000 0.000 0.000 0.000",
            "AhAd 1.000 0.000 0.000 0.000",
            "AsAd 1.000 0.000 0.000 0.000",
            "AsAh 1.000 0.000 0.000 0.000",
            "AcAh 1.000 0.000 0.000 0.000",
        ];
        factory
            .SolverConnectionFactory
            .Create(Arg.Any<SolverMetadata>())
            .Returns(factory.SolverConnection);
        factory.SolverConnection.GetResponseFromSolver($"{SolverCommands.ShowHumanReadableStratigy} r:0:c:b25:b125")
            .Returns(returnRange);
        var request = new FlopRangeRequest()
        {
            Position = PlayerPosition.BB,
            OOPFlopPlayerActions = [new PlayerAction(){ActionType = ActionType.Check}, new PlayerAction(){ActionType = ActionType.Raise, Size = 125}],
            IpFlopPlayerActions = [new PlayerAction(){ActionType = ActionType.Bet, Size = 25}],
            Flop = ["As", "Ac","Ah"]
        };
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(request),
            Encoding.UTF8,
            MediaTypes.ApplicationJson);
        //act
        var response = await _client.PostAsync("/range/get-flop", stringContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string[]>>(responseContent);
        
        //assert
        response.IsSuccessStatusCode.Should().BeTrue();
        apiResponse.IsSuccess.Should().BeTrue();
        apiResponse.Data.Should().BeEquivalentTo(returnRange);
        apiResponse.Errors.Should().BeNull();
    }

    [Fact]
    public async Task GetTurnRange_ShouldReturnCorrectRange_WhenCalled()
    {
        //arrange
        string[] returnRange =
        [
            "AsAd 1.000 0.000 0.000 0.000",
            "AcAd 1.000 0.000 0.000 0.000",
            "AhAd 1.000 0.000 0.000 0.000",
            "AsAd 1.000 0.000 0.000 0.000",
            "AsAh 1.000 0.000 0.000 0.000",
            "AcAh 1.000 0.000 0.000 0.000",
        ];
        factory
            .SolverConnectionFactory
            .Create(Arg.Any<SolverMetadata>())
            .Returns(factory.SolverConnection);
        factory.SolverConnection.GetResponseFromSolver($"{SolverCommands.ShowHumanReadableStratigy} r:0:c:c:6s")
            .Returns(returnRange);
        var request = new TurnRangeRequest()
        {
            Flop = [Card.AceClubs(), Card.AceDiamonds(), Card.AceHearts()],
            Position = PlayerPosition.BB,
            OOPFlopPlayerActions = [PlayerAction.Check()],
            IpFlopPlayerActions = [PlayerAction.Check()],
            TurnCard = Card.SixSpades()
        };
        var stringContent = new StringContent(
            JsonConvert.SerializeObject(request),
            Encoding.UTF8,
            MediaTypes.ApplicationJson);
        //act
        var apiResult = await _client.PostAsync("/range/get-turn", stringContent);
        //assert
        apiResult.IsSuccessStatusCode.Should().BeTrue();
        var content = await apiResult.Content.ReadAsStringAsync();
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string[]>>(content);
        apiResponse.Data.Should().BeEquivalentTo(returnRange);
        apiResponse.Errors.Should().BeNull();
    }

    [Fact]
    public void GetRiverRange_ShouldReturnCorrectRange_WhenCalled()
    {
        //arrange

        string[] returnRange =
        [
            "AsAd 1.000 0.000 0.000 0.000",
            "AcAd 1.000 0.000 0.000 0.000",
            "AhAd 1.000 0.000 0.000 0.000",
            "AsAd 1.000 0.000 0.000 0.000",
            "AsAh 1.000 0.000 0.000 0.000",
            "AcAh 1.000 0.000 0.000 0.000",
        ];
        factory
            .SolverConnectionFactory
            .Create(Arg.Any<SolverMetadata>())
            .Returns(factory.SolverConnection);
        factory.SolverConnection.GetResponseFromSolver($"{SolverCommands.ShowHumanReadableStratigy} r:0:c:c:6s:b50:c:2d")
            .Returns(returnRange);
        //act
        
        //assert
    }
    private readonly HttpClient _client = factory.CreateClient();
}