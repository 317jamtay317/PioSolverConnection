using System.ComponentModel;

namespace PioConnection.Dtos.Helpers;

public class EnumHelper
{
    /// <summary>
    /// Converts the Description of an enum into the Enum value.
    /// </summary>
    public static T? Parse<T>(string value) where T : Enum
    {
        // Check for Description attribute first
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == value)
                {
                    return (T?)field.GetValue(null);
                }
            }
        }

        // Try to match with the standard enum names (Enum.Parse) if no description matches
        if (Enum.TryParse(typeof(T), value, true, out var result))
        {
            return (T)result;
        }

        throw new ArgumentException($"No matching enum found for value: {value}");
    }

    /// <summary>
    /// Tries to convert the description into the value of type T,
    /// if it can convert it returns true, otherwise false.
    /// </summary>
    public static bool TryParse<T>(string value, out T? result) where T : struct, Enum
    {
        try
        {
            result = Parse<T>(value);
            return true;
        }
        catch (Exception)
        {
            result = null;
            return false;
        }
    }
}
