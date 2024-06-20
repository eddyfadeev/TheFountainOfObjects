using Services.Database.Interfaces;
using View.Interfaces;
using View.Views.MainMenu;

namespace View.Commands;

public class DisplayMainMenuCommand : ICommand
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;
    
    public DisplayMainMenuCommand(ILayoutManager layoutManager, IPlayerRepository playerRepository)
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
    }
    
    public Enum Execute()
    {
        var mainMenuView = new MainMenuView(_playerRepository, _layoutManager);

        return mainMenuView.DisplaySelectable();
    }
}