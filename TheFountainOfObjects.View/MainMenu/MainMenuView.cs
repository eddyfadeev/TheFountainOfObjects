using Spectre.Console;
using static TheFountainOfObjects.View.StartScreen.StartScreen;

namespace TheFountainOfObjects.View.MainMenu;

public class MainMenuView : MenuViews
{
    private static readonly IEnumerable<KeyValuePair<MainMenuEntries,string>> _mainMenuEntriesList = GetEnumValuesAndDisplayNames<MainMenuEntries>();
    private static readonly MainMenuView _mainMenuView = new ();

    private MainMenuView() : base("Main Menu")
    {
    }
    
    public static void ShowMainMenu()
    {
        _mainMenuView.ShowStartScreen();
        _layoutManager.SupportWindowIsVisible = true;
        var selectedEntry = _mainMenuView.ShowMenu(_mainMenuEntriesList);
        //_layoutManager.GameLayout["MainWindow"]
        
        InvokeActionForMenuEntry(selectedEntry, _mainMenuView);
    }

    // TODO: Fill up help menu with appropriate information.
    internal void ShowHelp()
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
    
    public void ShowStartScreen()
    {
        var startScreen = new Panel(ShowIntro().Expand());
        _layoutManager.SupportWindowIsVisible = false;

        _layoutManager.MainWindow.Update(startScreen);
        _layoutManager.UpdateLayout();
        Console.ReadKey();
    }
}