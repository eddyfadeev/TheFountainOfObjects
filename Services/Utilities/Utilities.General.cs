using System.ComponentModel.DataAnnotations;
using Spectre.Console;

namespace Services.Utilities;

public static partial class Utilities
{
    public static string GetUserInput(string message)
    {
        Console.CursorVisible = true;
        
        var userName = AnsiConsole.Ask<string>(message);

        Console.CursorVisible = false;
        
        return userName;
    }
    
    public static string ChangeStringColor(string text, Color color)
    {
        return $"[{color.ToString().ToLower()}]{text}[/]";
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
    
    public static void AddCaption(Table table)
    {
        table.Caption = new TableTitle(
            "\nPress any key to continue...",
            new Style(foreground: Color.White));
    }
}