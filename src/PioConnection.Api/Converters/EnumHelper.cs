using System.ComponentModel;

namespace PioConnection.Api.Converters;

public class EnumHelper
{
    public static T Parse<T>(string value) where T:Enum
    {
        // Try to match with the standard enum names (Enum.Parse)
        if (Enum.TryParse(typeof(T), value, true, out var result))
        {
            return (T)result;
        }

        // If no match found, check for Description attribute
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == value)
                {
                    return (T)field.GetValue(null);
                }
            }
        }

        throw new ArgumentException($"No matching enum found for value: {value}");
    }
    
    public static bool TryParse<T>(string value, out T result) where T : struct, Enum
    {
        try
        {
            result = Parse<T>(value);
            return true;
        }
        catch (Exception)
        {
            result = default;
            return false;
        }
    }
}