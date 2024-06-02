using View.GameLayout;

namespace View;

public abstract class MenuViewBase<TEnum> : IUpdatesLayout 
    where TEnum : Enum
{
    public static LayoutManager _layoutManager { get; } = new();
    protected abstract string MenuName { get; }

    private protected  TEnum ShowMenu(
        IEnumerable<KeyValuePair<TEnum, string>> menuEntries,
        bool? isDynamicallyGeneratedEnum = null,
        int? selectedEntry = null)
    {
        var entries = new List<KeyValuePair<TEnum, string>>(menuEntries);
        var isDynamicEnum = isDynamicallyGeneratedEnum ?? false;
        var selectedIndex = selectedEntry ?? 0;
        
        if (isDynamicEnum)
        {
            entries.Add(new KeyValuePair<TEnum, string>(default, "[bold white]To previous menu[/]"));
        }

        var selected = SelectEntry(ref entries, ref selectedIndex);

        return selected.Key;
    }

    private KeyValuePair<TEnum, string> SelectEntry(
        ref List<KeyValuePair<TEnum, string>> menuEntries, 
        ref int selectedIndex)
    {
        var userMadeChoice = false;
        KeyValuePair<TEnum, string> selected = new();
        
        while (!userMadeChoice)
        {
            RenderMenu(menuEntries, selectedIndex);
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : menuEntries.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectedIndex = (selectedIndex + 1) % menuEntries.Count;
            }
            else if (key == ConsoleKey.Enter)
            {
                selected = menuEntries[selectedIndex];
                userMadeChoice = true;
            }
            else if (key == ConsoleKey.Escape || selected.Value == "To previous menu")
            {
                userMadeChoice = true;
            }
        }
        
        return selected;
    }

    private void RenderMenu(
        List<KeyValuePair<TEnum,
            string>> entries, int selectedIndex)
    {
        var table = CreateMenuTable(entries, selectedIndex);
        _layoutManager.MainWindow.Update(table);

        _layoutManager.UpdateLayout();
    }

    private protected Table CreateTableLayout()
    {
        var table = new Table 
        {
            ShowHeaders = false,
            ShowFooters = false,
            Border = TableBorder.Rounded,
            Title = new TableTitle(
                MenuName,
                new Style(
                    foreground: Color.White,
                    decoration: Decoration.Bold
                )),
            Expand = true,
        };
        
        table.AddColumn(new TableColumn(MenuName).Centered());

        return table;
    }

    private Table CreateMenuTable(
        List<KeyValuePair<TEnum, string>> entries,
        int selectedIndex)
    {
        var table = CreateTableLayout();

        for (int i = 0; i < entries.Count; i++)
        {
            table.AddRow(i == selectedIndex
                ? $"[bold yellow]> {entries[i].Value}[/]"
                : $"[green]{entries[i].Value}[/]");
        }

        return table;
    }
}