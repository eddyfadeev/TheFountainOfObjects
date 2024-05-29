using Controller;
using View.StartScreen;

namespace Controller;

public class GameController
{
    private readonly StartScreen _startScreen = new();
    private readonly CreatePlayerController _createPlayerController = new();
    private readonly LoadPlayerController _loadPlayerController = new();
    private readonly MainMenuController _mainMenuController = new();
    
    public void StartGame()
    {
        _startScreen.ShowStartScreen();
        _createPlayerController.ShowCreatePlayerPrompt();
        _loadPlayerController.ShowLoadPlayerMenu();
        //Console.ReadKey();
        _mainMenuController.ShowMainMenu();
    }
}