using TheFountainOfObjects.View.StartScreen;

namespace TheFountainOfObjects.Controller;

public class GameController
{
    private readonly StartScreen _startScreen = new();
    private readonly CreatePlayerController _createPlayerController = new();
    private readonly MainMenuController _mainMenuController = new();
    
    public void StartGame()
    {
        _startScreen.ShowStartScreen();
        _createPlayerController.ShowCreatePlayerPrompt();
        _mainMenuController.ShowMainMenu();
    }
}