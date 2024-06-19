using View.Interfaces;
using Spectre.Console;

namespace View.Layout;

public sealed class LayoutManager : ILayoutManager
{
    private const string MainWindowName = "MainWindow";
    private const string SupportWindowName = "SupportWindow";
    private const string SupportNestedTopName = "Top";
    private const string SupportNestedBottomName = "Bottom";

    private Spectre.Console.Layout GameLayout { get; }
    public Spectre.Console.Layout MainWindow { get; }
    public Spectre.Console.Layout SupportWindowTop { get; }
    public Spectre.Console.Layout SupportWindowBottom { get; }
    public bool SupportWindowIsVisible { get; set; }
    
    public LayoutManager()
    {
        GameLayout = new Spectre.Console.Layout("Root")
            .SplitColumns(
                new Spectre.Console.Layout(MainWindowName),
                new Spectre.Console.Layout(SupportWindowName)
                    .SplitRows(
                        new Spectre.Console.Layout(SupportNestedTopName),
                        new Spectre.Console.Layout(SupportNestedBottomName)
                    )
            );

        MainWindow = GameLayout[MainWindowName];
        SupportWindowTop = GameLayout[SupportWindowName][SupportNestedTopName];
        SupportWindowBottom = GameLayout[SupportWindowName][SupportNestedBottomName];
        SupportWindowIsVisible = true;
        
        SetDefaultSize();
    }

    public void UpdateLayout()
    {
        Console.Clear();
        
        GameLayout[SupportWindowName].IsVisible = SupportWindowIsVisible;
        
        AnsiConsole.Write(GameLayout);
    }
    
    public Table CreateTableLayout(string menuName)
    {
        var table = new Table 
        {
            ShowHeaders = false,
            Border = TableBorder.Rounded,
            Expand = true,
            Title = new TableTitle(
                menuName,
                new Style(
                    foreground: Color.White,
                    decoration: Decoration.Bold
                )),
        };
        
        table.AddColumn(new TableColumn(menuName).Centered());

        return table;
    }
    
    private void SetDefaultSize()
    {
        const int mainWindowSize = 70;
        const int supportWindowSize = 30;
        
        GameLayout[MainWindowName].Size(mainWindowSize);
        GameLayout[MainWindowName].MinimumSize(mainWindowSize);
        GameLayout[SupportWindowName].Size(supportWindowSize);
        GameLayout[SupportWindowName].MinimumSize(supportWindowSize);
    }
}