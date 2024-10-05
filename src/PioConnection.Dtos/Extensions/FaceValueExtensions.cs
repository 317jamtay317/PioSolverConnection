namespace PioConnection.Dtos.Extensions;

public static class FaceValueExtensions
{
    public static string ToDisplayString(this FaceValue faceValue)
    {
        return faceValue switch
        {
            FaceValue.Ace => "A",
            FaceValue.King => "K",
            FaceValue.Queen => "Q",
            FaceValue.Jack => "J",
            FaceValue.Ten => "T",
            FaceValue.Nine => "9",
            FaceValue.Eight => "8",
            FaceValue.Seven => "7",
            FaceValue.Six => "6",
            FaceValue.Five => "5",
            FaceValue.Four => "4",
            FaceValue.Three => "3",
            FaceValue.Two => "2",
            _ => throw new ArgumentOutOfRangeException(nameof(faceValue), faceValue, null)
        };
    }
}