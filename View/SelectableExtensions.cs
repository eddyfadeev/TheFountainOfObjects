﻿namespace View;

public static class SelectableExtensions
{
    internal static TEnum SelectEntry<TEnum>(
        this ISelectable<TEnum> selectable,
        ref List<KeyValuePair<TEnum, string>> menuEntries)
        where TEnum : Enum
    {
        var userMadeChoice = false;
        KeyValuePair<TEnum, string> selected = new();
        
        AddEntryIfDynamicEnum(ref menuEntries);
        
        while (!userMadeChoice)
        {
            selectable.RenderMenu(menuEntries);
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                selectable.SelectedIndex = (selectable.SelectedIndex > 0) ? selectable.SelectedIndex - 1 : menuEntries.Count - 1;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                selectable.SelectedIndex = (selectable.SelectedIndex + 1) % menuEntries.Count;
            }
            else if (key == ConsoleKey.Enter)
            {
                selected = menuEntries[selectable.SelectedIndex];
                userMadeChoice = true;
            }
            else if (selected.Value == "To previous menu")
            {
                userMadeChoice = true;
            }
        }
        
        return selected.Key;
    }

    internal static void RenderMenu<TEnum>(
        this ISelectable<TEnum> selectable,
        List<KeyValuePair<TEnum, string>> menuEntries)
        where TEnum : Enum
    {
        var table = CreateMenuTable(selectable, selectable.MenuName, menuEntries);
        
        MenuViewBase._layoutManager.MainWindow.Update(table);
        MenuViewBase._layoutManager.UpdateLayout();
    }

    internal static Table CreateMenuTable<TEnum>(
        this ISelectable<TEnum> selectable,
        string menuName,
        List<KeyValuePair<TEnum, string>> menuEntries)
        where TEnum : Enum
    {
        var menuTable = SelectableMenuViewBase<TEnum>._layoutManager.CreateTableLayout(menuName);
        
        for (int i = 0; i < menuEntries.Count; i++)
        {
            menuTable.AddRow(i == selectable.SelectedIndex
                ? $"[bold yellow]> {menuEntries[i].Value}[/]"
                : $"[green]{menuEntries[i].Value}[/]");
        }

        return menuTable;
    }
    
    private static void AddEntryIfDynamicEnum<TEnum>(ref List<KeyValuePair<TEnum, string>> menuEntries)
        where TEnum : Enum
    {
        var assemblyName = menuEntries[0].Key.GetType().Assembly.GetName().Name;
        if (assemblyName == "DynamicEnum")
        {
            menuEntries.Add(new KeyValuePair<TEnum, string>(default!, "[bold white]To previous menu[/]"));
        }
    }
}