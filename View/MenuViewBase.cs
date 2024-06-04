using View.GameLayout;

namespace View;

public abstract class MenuViewBase<TEnum> : IUpdatesLayout 
    where TEnum : Enum
{
    public static LayoutManager _layoutManager { get; } = new();
    public abstract string MenuName { get; }

    /*private void RenderMenu(List<KeyValuePair<TEnum, string>> entries)
    {
        var table = CreateMenuTable(entries);
        _layoutManager.MainWindow.Update(table);

        _layoutManager.UpdateLayout();
    }*/

    /*private protected Table CreateTableLayout()
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
    }*/

    /*public Table CreateMenuTable(List<KeyValuePair<TEnum, string>> entries)
    {
        var table = _layoutManager.CreateTableLayout(MenuName);

        for (int i = 0; i < entries.Count; i++)
        {
            table.AddRow($"[green]{entries[i].Value}[/]");
        }

        return table;
    }*/
}