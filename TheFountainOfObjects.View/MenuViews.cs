global using static TheFountainOfObjects.Utilities.Utilities;

using Spectre.Console;
using Spectre.Console.Extensions;
using TheFountainOfObjects.View.GameLayout;
using TheFountainOfObjects.View.MainMenu;

namespace TheFountainOfObjects.View;

public abstract class MenuViews
{
    private protected static LayoutManager _layoutManager = new();
    private readonly string _menuName;

    protected MenuViews(string menuName)
    {
        _menuName = menuName;
    }

    private protected virtual MainMenuEntries ShowMenu(
        IEnumerable<KeyValuePair<MainMenuEntries, string>> menuEntries,
        int? selectedEntry = null)
    {
        var entries = new List<KeyValuePair<MainMenuEntries, string>>(menuEntries);
        int selectedIndex = selectedEntry ?? 0;
        bool isRunning = true;
        KeyValuePair<MainMenuEntries, string> selected = default;

        while (isRunning)
        {
            RenderMenu(entries, selectedIndex);
            var key = Console.ReadKey(true).Key;
            
            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : entries.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % entries.Count;
            }
            else if (key == ConsoleKey.Enter)
            {
                //AnsiConsole.Clear();
                selected = entries[selectedIndex];
                AnsiConsole.MarkupLine($"[green]You chose: {selected.Value}[/]");
                Console.ReadKey();
                isRunning = false;
                // Execute the corresponding method based on the enum value if needed
            }
        }
        
        return selected.Key;
    }

    private protected virtual void RenderMenu(
        List<KeyValuePair<MainMenuEntries, 
        string>> entries, int selectedIndex)
    {
        var table = CreateMenuTable(entries, selectedIndex);
        //var panelToRender = new Pa
        _layoutManager.MainWindow.Update(table);
        
        _layoutManager.UpdateLayout();
    }

    private protected virtual Align CreateMenuTable(
        List<KeyValuePair<MainMenuEntries, string>> entries,
        int selectedIndex)
    {
        var table = new Table()
        {
            ShowHeaders = false,
            Border = TableBorder.Rounded,
            Title = new TableTitle(
                _menuName, 
                new Style(
                foreground: Color.White, 
                decoration: Decoration.Bold
                )),
            //Width = 70,
            Expand = true,
        };
        
        table.AddColumn(new TableColumn(_menuName));

        for (int i = 0; i < entries.Count; i++)
        {
            if (i == selectedIndex)
            {
                table.AddRow($"[bold yellow]> {entries[i].Value}[/]");
            }
            else
            {
                table.AddRow($"{entries[i].Value}");
            }
        }

        Align aligner = new(table, HorizontalAlignment.Center, VerticalAlignment.Top);
        //aligner.MiddleAligned();
        
        return aligner;
    }
    
    // private protected static TEnum GetUserChoice<TEnum>(
    //     IEnumerable<KeyValuePair<TEnum, string>> menuEntries)
    //     where TEnum : struct, Enum
    // {
    //     AnsiConsole.Clear();
    //
    //     if (!menuEntries.Any())
    //     {
    //         AnsiConsole.WriteLine($"Problem reading menu entries.");
    //     }
    //
    //     var userChoice = AnsiConsole.Prompt(
    //         new SelectionPrompt<string>()
    //             .Title("Welcome to The Fountain of Objects!")
    //             .AddChoices(menuEntries.Select(e => e.Value))
    //     );
    //
    //     var selectedEntry = menuEntries.SingleOrDefault(e => e.Value == userChoice);
    //
    //     return selectedEntry.Key;
    // }
    
    
}