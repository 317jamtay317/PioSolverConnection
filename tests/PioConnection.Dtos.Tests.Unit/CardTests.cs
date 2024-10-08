using System.Collections;
using FluentAssertions;

namespace PioConnection.Dtos.Tests.Unit;

public class CardTests
{
    [Theory]
    [ClassData(typeof(CreateCardData))]
    public void CreateCard_ShouldCreateSpecifiedCard(Func<Card> createMethod, string expectedValue)
    {
        //arrange
        
        //act
        var card = createMethod();
        
        //assert
        card.Should().BeEquivalentTo((Card)expectedValue);
    }

    #region ClassMemberData

    public class CreateCardData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [ Card.AceSpades,    "As"];
            yield return [ Card.AceClubs,     "Ac"];
            yield return [ Card.AceDiamonds,  "Ad"];
            yield return [ Card.AceHearts,    "Ah"];
            yield return [ Card.KingSpades,   "Ks"];
            yield return [ Card.KingClubs,    "Kc"];
            yield return [ Card.KingDiamonds, "Kd"];
            yield return [ Card.QueenHearts,   "Qh"];
            yield return [ Card.QueenSpades,   "Qs"];
            yield return [ Card.QueenClubs,    "Qc"];
            yield return [ Card.QueenDiamonds, "Qd"];
            yield return [ Card.QueenHearts,   "Qh"];
            yield return [ Card.QueenHearts,   "Qh"];
            yield return [ Card.JackSpades,   "Js"];
            yield return [ Card.JackClubs,    "Jc"];
            yield return [ Card.JackDiamonds, "Jd"];
            yield return [ Card.JackHearts,   "Jh"];
            yield return [ Card.TenSpades,   "Ts"];
            yield return [ Card.TenClubs,    "Tc"];
            yield return [ Card.TenDiamonds, "Td"];
            yield return [ Card.TenHearts,   "Th"];
            yield return [ Card.NineSpades,   "9s"];
            yield return [ Card.NineClubs,    "9c"];
            yield return [ Card.NineDiamonds, "9d"];
            yield return [ Card.NineHearts,   "9h"];
            yield return [ Card.EightSpades,   "8s"];
            yield return [ Card.EightClubs,    "8c"];
            yield return [ Card.EightDiamonds, "8d"];
            yield return [ Card.EightHearts,   "8h"];
            yield return [ Card.SevenSpades,   "7s"];
            yield return [ Card.SevenClubs,    "7c"];
            yield return [ Card.SevenDiamonds, "7d"];
            yield return [ Card.SevenHearts,   "7h"];
            yield return [ Card.SixSpades,   "6s"];
            yield return [ Card.SixClubs,    "6c"];
            yield return [ Card.SixDiamonds, "6d"];
            yield return [ Card.SixHearts,   "6h"];
            yield return [ Card.FiveSpades,   "5s"];
            yield return [ Card.FiveClubs,    "5c"];
            yield return [ Card.FiveDiamonds, "5d"];
            yield return [ Card.FiveHearts,   "5h"];
            yield return [ Card.FourSpades,   "4s"];
            yield return [ Card.FourClubs,    "4c"];
            yield return [ Card.FourDiamonds, "4d"];
            yield return [ Card.FourHearts,   "4h"];
            yield return [ Card.ThreeSpades,   "3s"];
            yield return [ Card.ThreeClubs,    "3c"];
            yield return [ Card.ThreeDiamonds, "3d"];
            yield return [ Card.ThreeHearts,   "3h"];
            yield return [ Card.TwoSpades,   "2s"];
            yield return [ Card.TwoClubs,    "2c"];
            yield return [ Card.TwoDiamonds, "2d"];
            yield return [ Card.TwoHearts,   "2h"];
        }
        
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
    

    #endregion
}