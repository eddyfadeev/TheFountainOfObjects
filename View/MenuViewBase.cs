using View.GameLayout;

namespace View;

public abstract class MenuViewBase<TEnum> : IUpdatesLayout 
    where TEnum : Enum
{
    private protected static string _menuName;
    public static LayoutManager _layoutManager { get; } = new();

    private protected static TEnum ShowMenu(
        IEnumerable<KeyValuePair<TEnum, string>> menuEntries,
        bool isDynamicallyGeneratedEnum = false,
        int? selectedEntry = null)
    {
        var entries = new List<KeyValuePair<TEnum, string>>(menuEntries);
        var selectedIndex = selectedEntry ?? 0;
        var isRunning = true;
        KeyValuePair<TEnum, string> selected = default;
        
        if (isDynamicallyGeneratedEnum)
        {
            entries.Add(new KeyValuePair<TEnum, string>(default(TEnum), "[bold white]To previous menu[/]"));
        }

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
            else if (key == ConsoleKey.Escape || selected.Value == "To previous menu")
            {
                isRunning = false;
            }
        }

        return selected.Key;
    }

    private static void RenderMenu(
        List<KeyValuePair<TEnum,
            string>> entries, int selectedIndex)
    {
        var table = CreateMenuTable(entries, selectedIndex);
        _layoutManager.MainWindow.Update(table);

        _layoutManager.UpdateLayout();
    }

    private protected static Table CreateTableLayout()
    {
        var table = new Table 
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

        return table;
    }

    private static Table CreateMenuTable(
        List<KeyValuePair<TEnum, string>> entries,
        int selectedIndex)
    {
        var table = CreateTableLayout();

        for (int i = 0; i < entries.Count; i++)
        {
            if (i == selectedIndex)
            {
                table.AddRow($"[bold yellow]> {entries[i].Value}[/]");
            }
            else
            {
                table.AddRow($"[green]{entries[i].Value}[/]");
            }
        }

        return table;
    }
}