using View.MazeGenerator;
using View.Views.HelpScreen;

namespace View.Views.Game;

public class GameView : IGameView
{
    private readonly IMazeGeneratorService _mazeGeneratorService;
    public Table? Maze { get; private set; }
    public string MenuName { get; }
    public ILayoutManager LayoutManager { get; }
    
    public GameView(ILayoutManager layoutManager, IMazeGeneratorService mazeGeneratorService)
    {
        LayoutManager = layoutManager;
        MenuName = "The Fountain of Objects";
        _mazeGeneratorService = mazeGeneratorService;
    }
    
    public void Display()
    {
        var helpView = new HelpView(LayoutManager);
        var helpWindow = helpView.CreateHelpTable(HelpType.GameSideWindow);
        Maze = GenerateMazeTable();
        
        LayoutManager.SupportWindowIsVisible = true;
        LayoutManager.MainWindow.Update(Maze);
        LayoutManager.SupportWindowTop.Update(helpWindow);
        
        LayoutManager.UpdateLayout();
    }
    
    private Table GenerateMazeTable()
    {
        var maze = _mazeGeneratorService.CreateTable();
        var gameWindow = PrepareGameWindow(maze);
        
        return gameWindow;
    }
    
    public void UpdateMaze(Table maze)
    {
        var gameWindow = PrepareGameWindow(maze);
        
        Maze = gameWindow;
        
        LayoutManager.MainWindow.Update(Maze);
        
        LayoutManager.UpdateLayout();
    }

    private Table PrepareGameWindow(Table maze)
    {
        var table = LayoutManager.CreateTableLayout(MenuName);
        var gameTable = InitializeGameTable();
        
        gameTable.AddRow(maze);

        table.AddRow(gameTable);

        return table;
    }
    
    private Table InitializeGameTable()
    {
        var mazeTable = LayoutManager.CreateInnerTable();
        
        mazeTable.AddColumn("Game").Centered();
        mazeTable.ShowFooters();
        
        return mazeTable;
    }
}

