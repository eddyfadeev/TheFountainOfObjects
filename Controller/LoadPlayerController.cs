using Controller.Interfaces;
using Model.DataObjects;
using Model.Services;
using View.LoadPlayerMenu;

namespace Controller;

public sealed class LoadPlayerController : BaseController<Enum>, IGeneratesEnum
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

    public IEnumerable<PlayerDTO> GetDataForEnum() => new DatabaseManager().RetrievePlayers();
}