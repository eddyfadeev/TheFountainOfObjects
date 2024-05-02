using TheFountainOfObjects.View;

namespace TheFountainOfObjects.Controller;

//  TODO: Use Spectre.Console layout widget for displaying the game and status information.
// ! Left side is for the map, right top, for the player status, and right bottom for the game messages. 
class Program
{
    static void Main(string[] args)
    {
        MainMenuView.ShowMainMenu();
        
        /*var game = new Game();
        
        game.Start();*/
    }
}