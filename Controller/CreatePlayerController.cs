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
        var loadPlayer = new LoadPlayerController();
        bool isRunning = true;

        while (isRunning)
        {
            var selection = loadPlayer.ShowLoadPlayerMenu();
            
            if (selection is null || selection.Equals("To previous menu"))
            {
                isRunning = false;
                //ShowCreatePlayerPrompt();
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}