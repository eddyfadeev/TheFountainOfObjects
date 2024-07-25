namespace View.Extensions;

public static class TableExtensions
{
    public static Table AddCaption(this Table table)
    {
        table.Caption = new TableTitle(
            "\nPress any key to continue...",
            new Style(foreground: Color.White));

        return table;
    }
}