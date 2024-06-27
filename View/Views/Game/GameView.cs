using View.MazeGenerator;
using View.Views.HelpScreen;

namespace View.Views.Game;

public class GameView : IGameVIew
{
    private IMazeGeneratorService _mazeGeneratorService;
    public Table Maze { get; private set; }
    public string MenuName { get; }
    public ILayoutManager LayoutManager { get; }
    
    public GameView(ILayoutManager layoutManager, IMazeGeneratorService mazeGeneratorService)
    {
        LayoutManager = layoutManager;
        MenuName = "The Fountain of Objects";
        _mazeGeneratorService = mazeGeneratorService;
        Maze = CreateMazeTable();
    }
    
    public void Display()
    {
        var helpView = new HelpView(LayoutManager);
        var helpWindow = helpView.CreateHelpTable(HelpType.GameSideWindow);
        
        LayoutManager.SupportWindowIsVisible = true;
        LayoutManager.MainWindow.Update(Maze);
        LayoutManager.SupportWindowBottom.Update(helpWindow);
        
        LayoutManager.UpdateLayout();
    }

    public void UpdateMaze(Table maze)
    {
        Maze = maze;
        LayoutManager.MainWindow.Update(Maze);
        
        LayoutManager.UpdateLayout();
    }

    private Table CreateMazeTable()
    {
        var table = LayoutManager.CreateTableLayout(MenuName);
        var mazeTable = LayoutManager.CreateInnerTable();
        
        mazeTable.AddColumn("Game").Centered();
        mazeTable.ShowFooters();
        var maze = _mazeGeneratorService.CreateTable();
        
        mazeTable.AddRow(maze);

        table.AddRow(mazeTable);

        return table;
    }
}

