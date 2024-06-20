using View.Views.LoadPlayerMenu;

namespace View.Commands;

public class ShowLoadPlayerMenuCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;

    public ShowLoadPlayerMenuCommand(ILayoutManager layoutManager, IPlayerRepository playerRepository)
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