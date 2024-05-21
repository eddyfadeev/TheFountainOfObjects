global using static TheFountainOfObjects.Utilities.Utilities;

using Spectre.Console;

namespace TheFountainOfObjects.View;

public abstract class MenuViews
{
    private protected static TEnum GetUserChoice<TEnum>(
        IEnumerable<KeyValuePair<TEnum, string>> menuEntries)
        where TEnum : struct, Enum
    {
        AnsiConsole.Clear();

        if (!menuEntries.Any())
        {
            AnsiConsole.WriteLine($"Problem reading menu entries.");
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