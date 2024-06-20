/*using Controller.Interfaces;
using Model.Player;
using Services.Database.Interfaces;
using View.Views.LoadPlayerMenu;

namespace Controller;

public sealed class LoadPlayerController(IPlayerRepository playerRepository) : BaseController<Enum>, IGeneratesEnum
{
    public Enum? ShowLoadPlayerMenu()
    {
        var loadPlayerView = new LoadPlayerView(playerRepository);
        const string enumName = "LoadPlayerEnum";
        var enumData = playerRepository.GetAllPlayers();
        
        var entries = PrepareEnum(enumData, enumName);
        var selectedPlayer = loadPlayerView.ShowLoadPlayerMenu(entries);

        return selectedPlayer;
    }

    public List<PlayerDTO> GetDataForEnum() => playerRepository.GetAllPlayers();
}*/