using System.Collections;
using FluentAssertions;
using PioConnection.Api.Requests;
using PioConnection.Dtos;

namespace PioConnection.Api.Tests.Unit.Requests;

public class FlopRangeRequestTests
{
    [Theory]
    [ClassData(typeof(BuildFlopNodeStringData))]
    public void BuildNodString_ShouldBuildTheCorrectString_AccordingToTheActions(PlayerAction[] actions, string expectedString)
    {
        //arrange
        _sut.FlopActions = actions;
        
        //act
        var result = _sut.BuildNodeString();
        
        //assert
        result.Should().Be(expectedString);
    }

    #region Data Classes
    
    private class BuildFlopNodeStringData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new PlayerAction[] { }, "r:0"];
            yield return
            [
                new PlayerAction[]
                {
                    new(){ActionType = ActionType.Bet, Size = 100} 
                },
                "r:0:b100"
            ];
            yield return
            [
                new PlayerAction[]
                {
                    new(){ActionType = ActionType.Bet, Size = 100},
                    new(){ActionType = ActionType.Call},
                },
                "r:0:b100:c"
            ];
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    #endregion

    private readonly FlopRangeRequest _sut = new();
}