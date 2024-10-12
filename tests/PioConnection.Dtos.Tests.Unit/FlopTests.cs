using FluentAssertions;

namespace PioConnection.Dtos.Tests.Unit;

public class FlopTests
{
    [Fact]
    public void IsValidFlop_ShouldBeTrue_WhenWeHaveThreeCards()
    {
        //arrange
        Flop flop = [Card.AceClubs(), Card.AceDiamonds(), Card.AceSpades()];
        //act
        var isValid = flop.IsValidFlop();
        //assert
        isValid.Should().BeTrue();
    }

    [Fact]
    public void IsValidFlop_ShouldBeFalse_WhenWeHave3CardsButHave2OfTheSameCards()
    {
        //arrange
        Flop flop = [Card.AceClubs(), Card.AceClubs(), Card.AceDiamonds()];
        //act
        var isValid = flop.IsValidFlop();
        //assert
        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValidFlop_ShouldBeFalse_WhenWeDoNotHave3Cards()
    {
        //arrange
        Flop flop = [Card.AceClubs(), Card.AceDiamonds()];
        //act
        var isValid = flop.IsValidFlop();
        //assert
        isValid.Should().BeFalse();
    }
    
    [Fact]
    public void FirstCard_ShouldThrowNotSupportedException_WhenCardCountIsLessThan3()
    {
        //arrange
        Flop flop = new();
        flop.Add("As");
        flop.Add("Ac");
        Action action = () =>
        {
            var a = flop.FistCard;
        };
        //act

        //assert
        action.Should()
            .Throw<NotSupportedException>()
            .WithMessage("A flop must have 3 cards");
    }
    [Fact]
    public void SecondCard_ShouldThrowNotSupportedException_WhenCardCountIsLessThan3()
    {
        //arrange
        Flop flop = new();
        flop.Add("As");
        flop.Add("Ac");
        Action action = () =>
        {
            var a = flop.SecondCard;
        };
        //act

        //assert
        action.Should()
            .Throw<NotSupportedException>()
            .WithMessage("A flop must have 3 cards");
    }
    [Fact]
    public void ThirdCard_ShouldThrowNotSupportedException_WhenCardCountIsLessThan3()
    {
        //arrange
        Flop flop = new();
        flop.Add("As");
        flop.Add("Ac");
        Action action = () =>
        {
            var a = flop.SecondCard;
        };
        //act

        //assert
        action.Should()
            .Throw<NotSupportedException>()
            .WithMessage("A flop must have 3 cards");
    }
    [Fact]
    public void Add_ShouldAddCard_WhenLessThanThreeCards()
    {
        // Arrange
        var flop = new Flop();
        var card = new Card();

        // Act
        flop.Add(card);

        // Assert
        flop.Count.Should().Be(1);
    }

    [Fact]
    public void Add_ShouldThrowException_WhenAddingMoreThanThreeCards()
    {
        // Arrange
        var flop = new Flop();
        flop.Add(new Card());
        flop.Add(new Card());
        flop.Add(new Card());

        // Act
        Action act = () => flop.Add(new Card());

        // Assert
        act.Should().Throw<NotSupportedException>()
            .WithMessage("A flop can only have 3 cards");
    }

    [Fact]
    public void AddRange_ShouldAddCards_WhenLessThanOrEqualToThreeCards()
    {
        // Arrange
        var flop = new Flop();
        var cards = new[] { new Card(), new Card(), new Card() };

        // Act
        flop.AddRange(cards);

        // Assert
        flop.Count.Should().Be(3);
    }

    [Fact]
    public void AddRange_ShouldThrowException_WhenAddingMoreThanThreeCards()
    {
        // Arrange
        var flop = new Flop();
        var cards = new[] { new Card(), new Card(), new Card(), new Card() };

        // Act
        Action act = () => flop.AddRange(cards);

        // Assert
        act.Should().Throw<NotSupportedException>()
            .WithMessage("A flop can only have 3 cards");
    }

    [Fact]
    public void Clear_ShouldRemoveAllCards()
    {
        // Arrange
        var flop = new Flop();
        flop.Add(new Card());
        flop.Add(new Card());
        flop.Add(new Card());

        // Act
        flop.Clear();

        // Assert
        flop.Count.Should().Be(0);
    }

    [Fact]
    public void Contains_ShouldReturnTrue_WhenCardIsPresent()
    {
        // Arrange
        var flop = new Flop();
        var card = new Card();
        flop.Add(card);

        // Act
        var contains = flop.Contains(card);

        // Assert
        contains.Should().BeTrue();
    }

    [Fact]
    public void Contains_ShouldReturnFalse_WhenCardIsNotPresent()
    {
        // Arrange
        var flop = new Flop();
        var card = new Card();

        // Act
        var contains = flop.Contains(card);

        // Assert
        contains.Should().BeFalse();
    }

    [Fact]
    public void FistCard_ShouldReturnFirstCard_WhenThreeCardsExist()
    {
        // Arrange
        var flop = new Flop();
        var card1 = new Card();
        var card2 = new Card();
        var card3 = new Card();
        flop.Add(card1);
        flop.Add(card2);
        flop.Add(card3);

        // Act
        var result = flop.FistCard;

        // Assert
        result.Should().Be(card1);
    }

    [Fact]
    public void SecondCard_ShouldReturnSecondCard_WhenThreeCardsExist()
    {
        // Arrange
        var flop = new Flop();
        var card1 = new Card();
        var card2 = new Card();
        var card3 = new Card();
        flop.Add(card1);
        flop.Add(card2);
        flop.Add(card3);

        // Act
        var result = flop.SecondCard;

        // Assert
        result.Should().Be(card2);
    }

    [Fact]
    public void ThirdCard_ShouldReturnThirdCard_WhenThreeCardsExist()
    {
        // Arrange
        var flop = new Flop();
        var card1 = new Card();
        var card2 = new Card();
        var card3 = new Card();
        flop.Add(card1);
        flop.Add(card2);
        flop.Add(card3);

        // Act
        var result = flop.ThirdCard;

        // Assert
        result.Should().Be(card3);
    }

    [Fact]
    public void FistCard_ShouldThrowException_WhenLessThanThreeCards()
    {
        // Arrange
        var flop = new Flop();
        flop.Add(new Card());
        flop.Add(new Card());

        // Act
        Action act = () => _ = flop.FistCard;

        // Assert
        act.Should().Throw<NotSupportedException>()
            .WithMessage("A flop must have 3 cards");
    }

    [Fact]
    public void Remove_ShouldRemoveCard_WhenCardIsPresent()
    {
        // Arrange
        var flop = new Flop();
        var card = new Card();
        flop.Add(card);

        // Act
        var result = flop.Remove(card);

        // Assert
        result.Should().BeTrue();
        flop.Count.Should().Be(0);
    }

    [Fact]
    public void Remove_ShouldReturnFalse_WhenCardIsNotPresent()
    {
        // Arrange
        var flop = new Flop();
        var card = new Card();

        // Act
        var result = flop.Remove(card);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Indexer_ShouldReturnCardAtIndex()
    {
        // Arrange
        var flop = new Flop();
        var card1 = new Card();
        var card2 = new Card();
        var card3 = new Card();
        flop.Add(card1);
        flop.Add(card2);
        flop.Add(card3);

        // Act
        var result = flop[1];

        // Assert
        result.Should().Be(card2);
    }
}