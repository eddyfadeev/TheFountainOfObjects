using Services.Database.Interfaces;
using View.Interfaces;
using View.Views.LoadPlayerMenu;

namespace View.Commands;

public class DisplayLoadPlayerMenuCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;

    public DisplayLoadPlayerMenuCommand(ILayoutManager layoutManager, IPlayerRepository playerRepository)
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
    }

    public Enum? Execute()
    {
        var createPlayerView = new LoadPlayerView(_playerRepository, _layoutManager);
        return createPlayerView.DisplaySelectable();
    }
}