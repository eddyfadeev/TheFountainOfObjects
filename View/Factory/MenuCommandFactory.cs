using View.Commands;
using View.Views.HelpScreen;
using View.Views.Leaderboard;

namespace View.Factory;

public class MenuCommandFactory : IMenuCommandFactory
{
    private readonly ILayoutManager _layoutManager;
    private readonly IPlayerRepository _playerRepository;
    private readonly ISideMenu<HelpType> _helpSideView;
    private readonly ISideMenu<LeaderboardType> _leaderboardSideView;

    public MenuCommandFactory(
        ILayoutManager layoutManager, 
        IPlayerRepository playerRepository,
        ISideMenu<HelpType> helpSideView,
        ISideMenu<LeaderboardType> leaderboardSideView
        )
    {
        _layoutManager = layoutManager;
        _playerRepository = playerRepository;
        _helpSideView = helpSideView;
        _leaderboardSideView = leaderboardSideView;
    }

    public ICommand Create(MenuType menuType) => 
        menuType switch
        {
            MenuType.MainMenu => new ShowMainMenuCommand(_layoutManager, _helpSideView, _leaderboardSideView),
            MenuType.CreatePlayerMenu => new ShowCreatePlayerMenuCommand(_layoutManager),
            MenuType.LeaderboardMenu => new ShowLeaderboardCommand(_layoutManager, _playerRepository),
            MenuType.LoadPlayerMenu => new ShowLoadPlayerMenuCommand(_layoutManager, _playerRepository),
            MenuType.SettingsMenu => new ShowSettingsMenuCommand(_layoutManager),
            MenuType.StartScreen => new ShowStartScreenCommand(_layoutManager),
            MenuType.HelpMenu => new ShowHelpScreenCommand(_layoutManager),
            _ => throw new ArgumentException("Invalid menu type.")
        };
}