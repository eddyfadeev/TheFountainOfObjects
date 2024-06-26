using View.MazeGenerator;
using View.Views.HelpView;

namespace View.Views.GameView;

public class GameView : INonSelectableMenu, IGameVIew
{
    public Table Maze { get; private set; }
    public string MenuName { get; }
    public ILayoutManager LayoutManager { get; }
    
    public GameView(ILayoutManager layoutManager, IMazeGeneratorService mazeGeneratorService)
    {
        LayoutManager = layoutManager;
        MenuName = "Game";
        Maze = mazeGeneratorService.CreateTable();
    }
    
    public void Display()
    {
        var helpView = new HelpView.HelpView(LayoutManager);
        var helpWindow = helpView.CreateHelpWindow(HelpType.GameSideWindow);
        
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
}

