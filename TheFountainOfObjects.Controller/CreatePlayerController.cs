using Spectre.Console;
using TheFountainOfObjects.Model.GameObjects;
using TheFountainOfObjects.Model.Services;
using TheFountainOfObjects.View.CreatePlayerMenu;

namespace TheFountainOfObjects.Controller;

public class CreatePlayerController : BaseController<CreatePlayerEntries>
{
    public Player Player { get; private set; }
    public void ShowCreatePlayerPrompt()
    {
        CreatePlayerView.ShowCreatePlayerPrompt(OnMenuEntrySelected);
    }
    
    private void CreatePlayer()
    {
        Console.Clear();
        
        var name = AnsiConsole.Ask<string>("Please, enter your name > ");
        
        Player = new Player();
        Player.SetName(name);
    }
    
    private void LoadPlayer()
    {
        var databaseManager = new DatabaseManager();
        
        databaseManager.ShowPlayers();
        Console.ReadKey();
    }
}