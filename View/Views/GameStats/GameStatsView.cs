namespace View.Views.GameStats;

public class GameStatsView : INonSelectableMenu
{
    public string MenuName { get; }
    public ILayoutManager LayoutManager { get; }

    public GameStatsView(ILayoutManager layoutManager)
    {
        LayoutManager = layoutManager;
        MenuName = "Game Stats";
    }

    public void Display()
    {
        var gameStatsTable = LayoutManager.CreateTableLayout(MenuName);
        var gameStatsInnerTable = LayoutManager.CreateInnerTable();
        
        gameStatsInnerTable.AddColumn("[white bold]Game Stats[/]").Centered();
        
    }
}