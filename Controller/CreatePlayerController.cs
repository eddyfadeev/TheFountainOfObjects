using Spectre.Console;
using Model.GameObjects;
using Model.Services;
using View.CreatePlayerMenu;

namespace Controller;

public class CreatePlayerController : BaseController<CreatePlayerEntries>
{
    private readonly DatabaseManager _databaseManager = new();
    private readonly CreatePlayerView _createPlayerView = new();
    private Player? Player { get; set; }
    
    public Player ShowCreatePlayerPrompt()
    {
        bool playerIsCreated = false;

        while (!playerIsCreated)
        {
            _createPlayerView.ShowCreatePlayerPrompt(OnMenuEntrySelected);
            
            playerIsCreated = Player is not null;
        }
        
        return Player!;
    }
    
    public void CreatePlayer()
    {
        Console.Clear();
        
        bool existentName;
        string name;

        do
        {
            name = _createPlayerView.AskForUserName();
            existentName = _databaseManager.RetrievePlayers().Any(p => p.Name == name);
            
            if (existentName)
            {
                _createPlayerView.ShowAlreadyTakenMessage();
            }
            
        } while (existentName);
        
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

        Player = new Player(loadedPLayer.Name, (int)loadedPLayer.Score!)
        { 
            Id = (int)loadedPLayer.Id
        };
    }

    private void PreparePlayer(string name)
    {
        const int defaultScore = 0;
        
        Player = new Player(name, defaultScore);
    }
}