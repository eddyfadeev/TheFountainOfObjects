using View.Commands;

namespace View.Factory;

public class MenuCommandFactory : IMenuCommandFactory
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;

    public MenuCommandFactory(ILayoutManager layoutManager, IPlayerRepository playerRepository)
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
    }

    public ICommand Create(MenuType menuType) => 
        menuType switch
        {
            MenuType.MainMenu => new ShowMainMenuCommand(_layoutManager, _playerRepository),
            MenuType.CreatePlayerMenu => new ShowCreatePlayerMenuCommand(_layoutManager),
            MenuType.LeaderboardMenu => new ShowLeaderboardCommand(_layoutManager, _playerRepository),
            MenuType.LoadPlayerMenu => new ShowLoadPlayerMenuCommand(_layoutManager, _playerRepository),
            MenuType.SettingsMenu => new ShowSettingsMenuCommand(_layoutManager),
            MenuType.StartScreen => new ShowStartScreenCommand(_layoutManager),
            MenuType.HelpMenu => new ShowHelpScreenCommand(_layoutManager),
            _ => throw new ArgumentException("Invalid menu type.")
        };
}