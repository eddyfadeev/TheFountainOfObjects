using TheFountainOfObjects.Model.Services;
using TheFountainOfObjects.View.CreatePlayerMenu;
using TheFountainOfObjects.View.MainMenu;

namespace TheFountainOfObjects.Controller;
// ! Left side is for the map, right top, for the player status, and right bottom for the game messages. 
class Program
{
    static void Main(string[] args)
    {
        MainMenuView.ShowStartScreen();
        CreatePlayerView.ShowCreatePlayerPrompt();
        MainMenuView.ShowMainMenu();
        
        
        /*var game = new Game();
        
        game.Start();*/
    }
}