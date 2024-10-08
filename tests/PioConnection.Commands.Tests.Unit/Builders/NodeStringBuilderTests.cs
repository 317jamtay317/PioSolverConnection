using FluentAssertions;
using Newtonsoft.Json.Bson;
using PioConnection.Commands.Builders;
using PioConnection.Dtos;

namespace PioConnection.Commands.Tests.Unit.Builders;

public class NodeStringBuilderTests
{
    [Fact]
    public void ToString_ShouldReturnRootNode_WhenNoOptionsAreEnabled()
    {
        //arrange
        NodeStringBuilder builder = new();
        
        //act
        var nodeString = builder.ToString();
        
        //assert
        nodeString.Should().Be("r:0");
    }

    [Fact]
    public void ToString_ShouldAddTheAppropriateString_WhenActionsAreAdded()
    {
        //arrange
        NodeStringBuilder builder = new();
        builder.WithOOPFlopAction(new PlayerAction { ActionType = ActionType.Raise, Size = 25});
        
        //act
        var nodeString = builder.ToString();
        
        //assert
        nodeString.Should().Be("r:0:b25");
    }

    [Fact]
    public void ToString_ShouldUpdateTheIPstring_WhenAdded()
    {
        //arrange
        NodeStringBuilder builder = new NodeStringBuilder()
            .WithOOPFlopAction(new PlayerAction { ActionType = ActionType.Raise, Size = 25})
            .WithIPFlopAction(new PlayerAction() { ActionType = ActionType.Raise, Size = 125})
            .WithOOPFlopAction(new PlayerAction(){ActionType = ActionType.Call})
            .WithTurnCard(Card.TwoDiamonds());
        
        //act
        var nodeString = builder.ToString();
        
        //assert
        nodeString.Should().Be("r:0:b25:b125:c:2d");
    }

    [Fact]
    public void ToString_ShouldUpdateTheCorrectOrder_WheAddedMultipleActions()
    {
        //arrange
        var builder = new NodeStringBuilder()
            .WithOOPFlopAction(new PlayerAction() { ActionType = ActionType.Bet, Size = 25 })
            .WithIPFlopAction(new PlayerAction() { ActionType = ActionType.Raise, Size = 125 })
            .WithOOPFlopAction(new PlayerAction() { ActionType = ActionType.Call })
            .WithTurnCard(Card.KingClubs());
        
        //act
        var nodeString = builder.ToString();
        
        //assert
        nodeString.Should().Be("r:0:b25:b125:c:Kc");
    }
    [Fact]
    public void ToString_ShouldAddTurnCard_WhenTurnAdded()
    {
        //arrange
        var builder = new NodeStringBuilder()
            .WithOOPFlopAction(new PlayerAction() { ActionType = ActionType.Check})
            .WithIPFlopAction(new PlayerAction() { ActionType = ActionType.Check})
            .WithTurnCard(Card.SixHearts());
        
        //act
        var nodeString = builder.ToString();
        
        //assert
        nodeString.Should().Be("r:0:c:c:6h");
    }

    [Fact] 
    public void ToString_ShouldAllowMultipleActionsOnTurn_WhenAdded()
    {
        //arrange
        var builder = new NodeStringBuilder()
            .WithOOPFlopAction(new PlayerAction() { ActionType = ActionType.Check})
            .WithIPFlopAction(new PlayerAction() { ActionType = ActionType.Check})
            .WithTurnCard(Card.TenDiamonds())
            .WithOOPTurnAction(new PlayerAction(){ActionType = ActionType.Bet, Size = 25})
            .WithIPTurnAction(new PlayerAction(){ActionType = ActionType.Raise, Size = 125})
            .WithOOPTurnAction(new PlayerAction(){ActionType = ActionType.Call})
            .WithRiverCard(Card.NineHearts());

        //act
        var nodeString = builder.ToString();
        
        //assert
        nodeString.Should().Be("r:0:c:c:Td:b25:b125:c:9h");
    }

    [Fact]
    public void ToString_ShouldAddRiverCard_WhenRequested()
    {
        //arrange
        var builder = new NodeStringBuilder()
            .WithOOPFlopAction(new PlayerAction() { ActionType = ActionType.Check})
            .WithIPFlopAction(new PlayerAction() { ActionType = ActionType.Check})
            .WithTurnCard(Card.AceClubs())
            .WithOOPTurnAction(new PlayerAction(){ActionType = ActionType.Bet, Size = 25})
            .WithIPTurnAction(new PlayerAction(){ActionType = ActionType.Raise, Size = 125})
            .WithOOPTurnAction(new PlayerAction(){ActionType = ActionType.Call})
            .WithRiverCard(Card.FourSpades());
        
        //act
        var nodeString = builder.ToString();
        
        //assert
        nodeString.Should().Be("r:0:c:c:Ac:b25:b125:c:4s");
    }

    [Fact]
    public void ToString_ShouldThrow_WhenLastFlopActionIsCheckAndNoTurnIsSet()
    {
        //arrange
        var builder = new NodeStringBuilder()
            .WithOOPFlopAction(new PlayerAction() { ActionType = ActionType.Check })
            .WithIPFlopAction(new PlayerAction() { ActionType = ActionType.Check });
        Action action = () => builder.ToString();
        //act

        //assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("The last action must be a call and the next card must exist");
    }
    [Fact]
    public void ToString_ShouldThrow_WhenLastFlopActionIsCallAndNoTurnIsSet()
    {
        //arrange
        var builder = new NodeStringBuilder()
            .WithOOPFlopAction(new PlayerAction() { ActionType = ActionType.Bet, Size = 33})
            .WithIPFlopAction(new PlayerAction() { ActionType = ActionType.Call });
        Action action = () => builder.ToString();
        //act

        //assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("The last action must be a call and the next card must exist");
    }
}