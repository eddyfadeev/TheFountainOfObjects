namespace View;

public interface ISelectable<TEnum>
where TEnum : Enum
{
    TEnum SelectEntry(
        ref List<KeyValuePair<TEnum, string>> menuEntries);
    
    void RenderMenu(
        List<KeyValuePair<TEnum, string>> menuEntries);

    Table CreateMenuTable(
        string menuName,
        List<KeyValuePair<TEnum, string>> menuEntries);
}

public static class SelectableExtensions
{
    public static TEnum SelectEntry<TEnum>(
        this ISelectable<TEnum> selectable,
        ref List<KeyValuePair<TEnum, string>> menuEntries)
        where TEnum : Enum
    {
        var userMadeChoice = false;
        KeyValuePair<TEnum, string> selected = new();
        
        while (!userMadeChoice)
        {
            selectable.RenderMenu(menuEntries);
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
        
        return selected.Key;
    }

    public static void RenderMenu<TEnum>(
        this ISelectable<TEnum> selectable,
        List<KeyValuePair<TEnum, string>> menuEntries,
        int selectedIndex)
        where TEnum : Enum
    {
        var table = selectable.CreateMenuTable(menuEntries, selectedIndex);
        MenuViewBase<TEnum>._layoutManager.MainWindow.Update(table);
        MenuViewBase<TEnum>._layoutManager.UpdateLayout();
    }

    public static Table CreateMenuTable<TEnum>(
        this ISelectable<TEnum> selectable,
        string menuName,
        List<KeyValuePair<TEnum, string>> menuEntries)
        where TEnum : Enum
    {
        var table = MenuViewBase<TEnum>._layoutManager.CreateTableLayout(menuName);
        
        for (int i = 0; i < menuEntries.Count; i++)
        {
            table.AddRow(i == selectedIndex
                ? $"[bold yellow]> {menuEntries[i].Value}[/]"
                : $"[green]{menuEntries[i].Value}[/]");
        }

        return table;
    }
}