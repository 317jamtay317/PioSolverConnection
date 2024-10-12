namespace PioConnection.Dtos.Extensions;

public static class PlayerPositionExtensions
{
    /// <summary>
    /// returns the number that the player acts preflop, UTG is always 0 and the BB is 7
    /// </summary>
    public static int PreflopOrder(this PlayerPosition position)
    {
        return position switch
        {
            PlayerPosition.UTG => 0,
            PlayerPosition.UTG1 => 1,
            PlayerPosition.LJ => 2,
            PlayerPosition.HJ => 3,
            PlayerPosition.CO => 4,
            PlayerPosition.BTN => 5,
            PlayerPosition.SB => 6,
            PlayerPosition.BB => 7,
        }; 
    }
}