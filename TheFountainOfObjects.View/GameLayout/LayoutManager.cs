using Spectre.Console;
using Spectre.Console.Rendering;

namespace TheFountainOfObjects.View.GameLayout;

public class LayoutManager
{
    private const string MainWindowName = "MainWindow";
    private const string SupportWindowName = "SupportWindow";
    private const string SupportNestedTopName = "Top";
    private const string SupportNestedBottomName = "Bottom";

    public Layout GameLayout { get; }
    public Layout MainWindow { get; set; }
    public Layout SupportWindowTop { get; set; }
    public Layout SupportWindowBottom { get; set; }
    internal bool SupportWindowIsVisible { get; set; }
    
    public LayoutManager()
    {
        GameLayout = new Layout("Root")
            .SplitColumns(
                new Layout(MainWindowName),
                new Layout(SupportWindowName)
                    .SplitRows(
                        new Layout(SupportNestedTopName),
                        new Layout(SupportNestedBottomName)
                    )
            );

        MainWindow = GameLayout[MainWindowName];
        SupportWindowTop = GameLayout[SupportWindowName][SupportNestedTopName];
        SupportWindowBottom = GameLayout[SupportWindowName][SupportNestedBottomName];
        SupportWindowIsVisible = true;
        
        SetDefaultSize();
    }

    static LayoutManager()
    {
        
    }

    public void UpdateLayout()
    {
        Console.Clear();
        
        GameLayout[SupportWindowName].IsVisible = SupportWindowIsVisible;
        
        AnsiConsole.Write(GameLayout);
        //AnsiConsole.WriteLine();
        //Console.ReadKey();
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