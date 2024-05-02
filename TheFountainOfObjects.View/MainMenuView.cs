using Spectre.Console;
using static TheFountainOfObjects.Utilities.Utilities;

namespace TheFountainOfObjects.View;

public static class MainMenuView
{
    public static MenuEntries ShowMainMenu()
    {
        var mainMenuEntries = GetEnumValuesAndDisplayNames<MenuEntries>();

        return ShowMenu(mainMenuEntries);
    }

    private static TEnum ShowMenu<TEnum>(
        IEnumerable<KeyValuePair<TEnum, string>> menuEntries)
        where TEnum : struct, Enum
    {
        AnsiConsole.Clear();

        if (!menuEntries.Any())
        {
            AnsiConsole.WriteLine($"Problem reading menu entries.");
            //selectedEntry = default;
        }

        var userChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to The Fountain of Objects!")
                .AddChoices(menuEntries.Select(e => e.Value))
            );

        var selectedEntry = menuEntries.SingleOrDefault(e => e.Value == userChoice);

        return selectedEntry.Key;
    }
}