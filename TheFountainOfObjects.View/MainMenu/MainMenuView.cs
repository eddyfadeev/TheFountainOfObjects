using Spectre.Console;
using TheFountainOfObjects.View.MainMenu;

namespace TheFountainOfObjects.View;

public class MainMenuView : MenuViews
{
    private static readonly IEnumerable<KeyValuePair<MainMenuEntries,string>> _mainMenuEntriesList = GetEnumValuesAndDisplayNames<MainMenuEntries>();
    private static readonly MainMenuView _mainMenuView = new();
    
    public static void ShowMainMenu()
    {
        AnsiConsole.Clear();
        
        var selectedEntry = GetUserChoice(_mainMenuEntriesList);
        
        InvokeActionForMenuEntry(selectedEntry, _mainMenuView);
    }

    // TODO: Fill up help menu with appropriate information.
    public static void ShowHelp()
    {
        Console.Clear();
        Console.WriteLine("HELP");
        Console.WriteLine("----------------------------------");
        Console.WriteLine("The following commands are available:");
        Console.WriteLine("move {direction} - moves the player to the specified direction");
        Console.WriteLine("shoot {direction} - shoots an arrow to the specified direction");
        Console.WriteLine("help - prints this help information");
        Console.WriteLine("exit - exits the game\n");
        Console.WriteLine("Available directions: north, south, east, west\n");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}