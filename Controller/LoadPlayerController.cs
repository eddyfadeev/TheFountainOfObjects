using Controller.Interfaces;
using Model.Player;
using Services.Database.Interfaces;
using View.LoadPlayerMenu;

namespace Controller;

public sealed class LoadPlayerController(IDatabaseService databaseService) : BaseController<Enum>, IGeneratesEnum
{
    public Enum? ShowLoadPlayerMenu()
    {
        const string enumName = "LoadPlayerEnum";
        var loadPlayerView = new LoadPlayerView();
        var enumData = GetDataForEnum();
        
        var entries = PrepareEnum(enumData, enumName);
        var selectedPlayer = loadPlayerView.ShowLoadPlayerMenu(entries);

        return selectedPlayer;
    }

    public List<PlayerDTO> GetDataForEnum() => databaseService.GetAllPlayers();
}