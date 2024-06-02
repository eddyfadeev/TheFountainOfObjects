using Spectre.Console;
using Model.GameObjects;
using Model.Services;
using View.CreatePlayerMenu;

namespace Controller;

public class CreatePlayerController : BaseController<CreatePlayerEntries>
{
    private DatabaseManager _databaseManager = new();
    private Player? Player { get; set; }
    
    public Player ShowCreatePlayerPrompt()
    {
        var createPlayerView = new CreatePlayerView();
        bool playerIsCreated = false;

        while (!playerIsCreated)
        {
            createPlayerView.ShowCreatePlayerPrompt(OnMenuEntrySelected);
            
            playerIsCreated = Player is not null;
        }
        
        return Player!;
    }
    
    private void CreatePlayer()
    {
        Console.Clear();

        var existentName = true;
        var name = string.Empty;
        
        while (existentName)
        {
            name = AnsiConsole.Ask<string>("Please, enter your name > ");
            existentName = _databaseManager.RetrievePlayers().Any(p => p.Name == name);
            
            if (existentName)
            {
                AnsiConsole.MarkupLine("[red]This name is already taken. Please, choose another one.[/]");
            }
        }
        
        PreparePlayer(name);
    }
    
    private void LoadPlayer()
    {
        var loadPlayer = new LoadPlayerController();

        var selection = loadPlayer.ShowLoadPlayerMenu();
        
        if (selection is not null)
        {
            PreparePlayer(selection);
        }
        else
        {
            ShowCreatePlayerPrompt();
        }
    }

    private void PreparePlayer(Enum selectedEntry)
    {
        var playerIdToLoad = Convert.ToInt64(selectedEntry);
        var loadedPLayer = _databaseManager.LoadPlayer(playerIdToLoad);

        Player = new Player(loadedPLayer.Name, (int)loadedPLayer.Score!);
        {
            Player.Id = (int)loadedPLayer.Id;
        }
    }

    private void PreparePlayer(string name)
    {
        const int defaultScore = 0;
        
        Player = new Player(name, defaultScore);
    }
}