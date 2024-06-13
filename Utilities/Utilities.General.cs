using System.Reflection;
using Model.Services;
using Spectre.Console;

namespace Utilities;

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
    
    public static void InvokeActionForMenuEntry(Enum entry, object actionInstance)
    {
        var entryFieldInfo = entry.GetType().GetField(entry.ToString());
        var methodAttribute = entryFieldInfo.GetCustomAttribute<MethodAttribute>();

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
    
    public static void AddCaption(ref Table table)
    {
        table.Caption = new TableTitle(
            "\nPress any key to continue...",
            new Style(foreground: Color.White));
    }
    
    public static bool IsNameTaken(string name)
    {
        var databaseManager = new DatabaseManager();
        return databaseManager.RetrievePlayers().Any(p => p.Name == name);
    }
}