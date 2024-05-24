using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
    
    public static void InvokeActionForMenuEntry(Enum entry, object actionInstance)
    {
        var entryFieldInfo = entry.GetType().GetField(entry.ToString());
        var methodAttribute = entryFieldInfo.GetCustomAttribute<CustomEnumAttributes.MethodAttribute>();

        if (methodAttribute != null)
        {
            var method = actionInstance.GetType().GetMethod(
                methodAttribute.MethodName, 
                BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance
            );

            if (method != null)
            {
                method.Invoke(actionInstance, null);
            }
            else
            {
                Console.WriteLine($"Method '{methodAttribute.MethodName}' not found.");
            }
        }
        else
        {
            Console.WriteLine($"No methods assigned for {entry}.");
        }
    }
    
    //public static 
}