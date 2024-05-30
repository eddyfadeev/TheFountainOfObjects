using Spectre.Console;
using Model.GameObjects;
using Model.Services;
using View.CreatePlayerMenu;

namespace Controller;

public class CreatePlayerController : BaseController<CreatePlayerEntries>
{
    public Player Player { get; private set; }
    
    public void ShowCreatePlayerPrompt()
    {
        bool playerIsCreated = false;

        while (!playerIsCreated)
        {
            CreatePlayerView.ShowCreatePlayerPrompt(OnMenuEntrySelected);
            
            playerIsCreated = Player is not null;
        }
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
        var loadPlayer = new LoadPlayerController();

        var selection = loadPlayer.ShowLoadPlayerMenu();
        
        if (selection is not null)
        {
            var databaseManager = new DatabaseManager();
            Player = new Player();
            
            //Player = databaseManager.LoadPlayer(selection.)
            
            Player.SetName(selection.ToString());
        }
        
        /*
        while (isRunning)
        {
            
            
            if (selection is null || selection.Equals("To previous menu"))
            {
                isRunning = false;
                ShowCreatePlayerPrompt();
            }
            else
            {
                throw new NotImplementedException();
            }
        }*/
    }
}