using Controller.Interfaces;
using Model.DataObjects;
using Model.Services;
using View.LoadPlayerMenu;

namespace Controller;

public sealed class LoadPlayerController : BaseController<Enum>, IGeneratesEnum
{
    public Enum? SelectedPlayer { get; private set; }

    public override void ShowMenu()
    {
        const string enumName = "LoadPlayerEnum";
        var loadPlayerView = new LoadPlayerView();
        var enumData = GetDataForEnum();
        
        var entries = PrepareEnum(enumData, enumName);
        SelectedPlayer = loadPlayerView.ShowLoadPlayerMenu(entries);
    }

    public IEnumerable<PlayerDTO> GetDataForEnum() => new DatabaseManager().RetrievePlayers();
}