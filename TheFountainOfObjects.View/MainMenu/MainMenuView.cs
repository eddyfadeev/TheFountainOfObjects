﻿using static TheFountainOfObjects.View.StartScreen.StartScreen;

namespace TheFountainOfObjects.View.MainMenu;

public sealed class MainMenuView : MenuViewBase<MainMenuEntries>
{
    private static readonly IEnumerable<KeyValuePair<MainMenuEntries,string>> _mainMenuEntriesList = GetEnumValuesAndDisplayNames<MainMenuEntries>();
    private static readonly MainMenuView Instance = new("Main Menu");
    
    private MainMenuView(string menuName) : base(menuName)
    {
    }
    
    public static void ShowMainMenu(Action<MainMenuEntries> onMenuEntrySelected)
    {
        _layoutManager.SupportWindowIsVisible = true;
        var selectedEntry = ShowMenu(_mainMenuEntriesList);
        
        onMenuEntrySelected(selectedEntry);
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
}