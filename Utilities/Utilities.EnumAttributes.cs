using System.ComponentModel.DataAnnotations;

namespace Utilities;

public static partial class Utilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class MethodAttribute(string methodName) : Attribute
    {
        public string MethodName { get; } = methodName;
    }
    
    public static List<KeyValuePair<TEnum, string>>
        GetEnumValuesAndDisplayNames<TEnum>()
        where TEnum : Enum
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
            )).ToList();
    }
}