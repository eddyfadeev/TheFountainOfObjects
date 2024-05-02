using System.ComponentModel.DataAnnotations;

namespace TheFountainOfObjects.Utilities;

public static class Utilities
{
    public static IEnumerable<KeyValuePair<TEnum, string>>
        GetEnumValuesAndDisplayNames<TEnum>()
        where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(enumValue => new KeyValuePair<TEnum, string>(
                enumValue,
                enumValue.GetType()
                    .GetField(enumValue.ToString())
                    ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                    .Cast<DisplayAttribute>()
                    .FirstOrDefault()?.Name ?? enumValue.ToString()
            ));
    } 
}