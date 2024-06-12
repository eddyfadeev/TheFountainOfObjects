using View.CreatePlayerMenu;
using View.Leaderboard;
using View.StartScreen;

namespace Controller;

public class GameController
{
    private readonly StartScreen _startScreen = new();
    private readonly CreatePlayerController _createPlayerController = new();
    private readonly MainMenuController _mainMenuController = new();
    
    public void StartGame()
    {
        Console.CursorVisible = false;
        _startScreen.ShowStartScreen();
        _createPlayerController.ShowMenu();
        
        _mainMenuController.ShowMenu();
    }
}