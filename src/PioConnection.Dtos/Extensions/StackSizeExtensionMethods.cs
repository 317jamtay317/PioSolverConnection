namespace PioConnection.Dtos.Extensions;

public static class StackSizeExtensionMethods
{
    public static string? ToFilePathString(this StackSize stackSize)
    {
        return stackSize switch
        {
            StackSize.NotDefined => null,
            StackSize._10 => "10BB",
            StackSize._15 => "15BB",
            StackSize._20 => "20BB",
            StackSize._25 => "25BB",
            StackSize._30 => "30BB",
            StackSize._35 => "35BB",
            StackSize._40 => "40BB",
            StackSize._50 => "50BB",
            StackSize._60 => "60BB",
            StackSize._80 => "80BB",
            StackSize._100 => "100BB",
            StackSize._150 => "150BB",
            StackSize._200 => "200BB",
            _ => throw new ArgumentOutOfRangeException(nameof(stackSize), stackSize, null)
        };
    }
}