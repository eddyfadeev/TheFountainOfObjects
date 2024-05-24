using TheFountainOfObjects.View.GameLayout;
using TheFountainOfObjects.View.MainMenu;

namespace TheFountainOfObjects.View;

public abstract class MenuViewBase<TEnum> where TEnum : Enum
{
    private protected static readonly LayoutManager _layoutManager = new();
    private readonly string _menuName;
    
    protected MenuViewBase(string menuName)
    {
        _menuName = menuName;
    }

    private protected TEnum ShowMenu(
        IEnumerable<KeyValuePair<TEnum, string>> menuEntries,
        int? selectedEntry = null)
    {
        var entries = new List<KeyValuePair<TEnum, string>>(menuEntries);
        var selectedIndex = selectedEntry ?? 0;
        var isRunning = true;
        KeyValuePair<TEnum, string> selected = default;

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
                selected = entries[selectedIndex];
                isRunning = false;
            }
        }

        return selected.Key;
    }

    private void RenderMenu(
        List<KeyValuePair<TEnum,
            string>> entries, int selectedIndex)
    {
        var table = CreateMenuTable(entries, selectedIndex);
        _layoutManager.MainWindow.Update(table);

        _layoutManager.UpdateLayout();
    }

    private Table CreateMenuTable(
        List<KeyValuePair<TEnum, string>> entries,
        int selectedIndex)
    {
        var table = new Table()
        {
            ShowHeaders = false,
            ShowFooters = false,
            Border = TableBorder.Rounded,
            Title = new TableTitle(
                _menuName,
                new Style(
                    foreground: Color.White,
                    decoration: Decoration.Bold
                )),
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

        return table;
    }
}