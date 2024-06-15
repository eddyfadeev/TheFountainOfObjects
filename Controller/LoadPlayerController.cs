using Controller.Interfaces;
using DataObjects.Player;
using Services.Database.Interfaces;
using View.LoadPlayerMenu;

namespace Controller;

public sealed class LoadPlayerController : BaseController<Enum>, IGeneratesEnum
{
    private IDatabaseService _databaseService;
    
    public Enum? ShowLoadPlayerMenu()
    {
        const string enumName = "LoadPlayerEnum";
        var loadPlayerView = new LoadPlayerView();
        var enumData = GetDataForEnum();
        
        var entries = PrepareEnum(enumData, enumName);
        var selectedPlayer = loadPlayerView.ShowLoadPlayerMenu(entries);

        return selectedPlayer;
    }

    public IEnumerable<PlayerDTO> GetDataForEnum() => _databaseService.RetrievePlayers();
}